﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using SproutSocial.Application.Abstractions.Email;
using SproutSocial.Application.Abstractions.Services;
using SproutSocial.Application.DTOs.MailDtos;
using SproutSocial.Application.DTOs.UserDtos;
using SproutSocial.Application.Exceptions.Authentication;
using SproutSocial.Application.Exceptions.Blogs;
using SproutSocial.Application.Exceptions.Users;
using SproutSocial.Application.Helpers;
using SproutSocial.Application.Helpers.Extesions;
using SproutSocial.Domain.Entities.Identity;
using SproutSocial.Persistence.Enums;
using System.Net;

namespace SproutSocial.Persistence.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly LinkGenerator _linkGenerator;
    private readonly IMailService _mailService;
    private readonly IWebHostEnvironment _environment;

    public UserService(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, LinkGenerator linkGenerator
        , IMailService mailService, IWebHostEnvironment environment)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
        _linkGenerator = linkGenerator;
        _mailService = mailService;
        _environment = environment;
    }

    public async Task<AddUserTopicReponseDto> AddUserTopicsAsync(List<string> topicIds)
    {
        var userInfo = _httpContextAccessor?.HttpContext?.User?.Identity;
        if (userInfo?.IsAuthenticated == true)
        {
            var user = await _userManager.FindByNameAsync(userInfo.Name);
            List<UserTopic> userTopics = new();

            foreach (var topicId in topicIds)
            {
                userTopics.Add(new()
                {
                    TopicId = Guid.Parse(topicId),
                    AppUserId = user.Id
                });
            }
            user.UserTopics = userTopics;

            await _userManager.UpdateAsync(user);

            return new()
            {
                IsSuccess = true,
                Message = "Topic successfully added to user"
            };
        }

        throw new AuthorizationException("User not logged in", HttpStatusCode.Unauthorized);
    }

    public async Task<CreateUserResponseDto> CreateAsync(CreateUserDto model)
    {
        AppUser user = new()
        {
            Fullname = model.Fullname,
            UserName = model.Username,
            Email = model.Email
        };

        IdentityResult result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, Roles.Member.ToString());

            string? url = await GetEmailConfirmationLinkAsync(user);
            string body = GetEmailConfirmationTemplate(url);

            await _mailService.SendEmailAsync(new MailRequestDto { ToEmail = user.Email, Subject = "SproutSocial email confirmation for activate account", Body = body });

            return new()
            {
                IsSuccess = true,
                Message = "User successfully created. To login to your account, please activate your account by clicking on the link sent to your email address."
            };
        }

        throw new UserCreateFailedException(result.Errors);
    }

    public async Task<bool> SaveBlogAsync(string blogId)
    {
        ArgumentNullException.ThrowIfNull(blogId);

        var user = _httpContextAccessor.HttpContext.User.Identity;
        if (!user.IsAuthenticated)
            throw new AuthorizationException("Please login to save any post", HttpStatusCode.Unauthorized);

        var dbUser = await _userManager.FindByNameAsync(user.Name);
        if (dbUser is null)
            throw new UserNotFoundException(nameof(user.Name), user.Name);

        var blog = await _unitOfWork.BlogReadRepository.GetSingleAsync(b => b.Id == Guid.Parse(blogId) && !b.IsDeleted, tracking: false);
        if (blog is null)
            throw new BlogNotFoundByIdException(Guid.Parse(blogId));

        SavedBlog savedBlog = new()
        {
            BlogId = blog.Id,
            AppUserId = dbUser.Id,
        };

        dbUser.SavedBlogs = dbUser.SavedBlogs ?? new List<SavedBlog>();
        dbUser.SavedBlogs.Add(savedBlog);

        var identityResult = await _userManager.UpdateAsync(dbUser);

        return identityResult.Succeeded;
    }

    public async Task<bool> RemoveSavedBlogAsync(string blogId)
    {
        ArgumentNullException.ThrowIfNull(blogId);

        var user = _httpContextAccessor.HttpContext.User.Identity;
        if (!user.IsAuthenticated)
            throw new AuthorizationException("Please login to remove saved any post", HttpStatusCode.Unauthorized);

        var dbUser = await _userManager.FindByNameAsync(user.Name);
        if (dbUser is null)
            throw new UserNotFoundException(nameof(user.Name), user.Name);

        var blog = await _unitOfWork.BlogReadRepository.GetSingleAsync(b => b.Id == Guid.Parse(blogId) && !b.IsDeleted, tracking: true, "SavedBlogs");
        if (blog is null)
            throw new BlogNotFoundByIdException(Guid.Parse(blogId));

        var savedBlog = blog.SavedBlogs.FirstOrDefault(b => b.BlogId == Guid.Parse(blogId) && b.AppUserId == dbUser.Id);
        if (savedBlog is null)
            throw new BlogNotFoundException("Saved blog not found");

        blog.SavedBlogs.Remove(savedBlog);

        var result = _unitOfWork.BlogWriteRepository.Update(blog);
        await _unitOfWork.SaveAsync();

        return result;
    }

    public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenEndDate, int refreshTokenLifeTime)
    {
        if (user != null)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenEndDate = accessTokenEndDate.AddMinutes(refreshTokenLifeTime);

            await _userManager.UpdateAsync(user);
            return;
        }

        throw new UserNotFoundException("User cannot be null");
    }

    private async Task<string?> GetEmailConfirmationLinkAsync(AppUser user)
    {
        string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var httpContext = _httpContextAccessor.HttpContext;
        var request = httpContext.Request;

        string? url = _linkGenerator.GetUriByAction(
            httpContext,
            action: "ConfirmEmail",
            controller: "Users",
            values: new { token, email = user.Email },
            scheme: request.Scheme,
            host: request.Host
        );

        return url;
    }

    private string GetEmailConfirmationTemplate(string url)
    {
        var filePath = $"{_environment.WebRootPath}/templates/EmailConfirmation.html";

        StreamReader streamReader = new StreamReader(filePath);
        string mailText = streamReader.ReadToEnd();
        streamReader.Close();

        mailText = mailText.Replace("[ConfirmationLink]", url);

        return mailText;
    }

    public async Task<SelectTwoFaMethodResponseDto> SelectTwoFaMethodAsync(SelectTwoFaMethodDto selectTwoFaMethodDto)
    {
        selectTwoFaMethodDto.UserId.ThrowIfNullOrWhiteSpace(message: "Token cannot be null");
        selectTwoFaMethodDto.TwoFaMethodId.ThrowIfNullOrWhiteSpace(message: "Email cannot be null");

        var user = await _userManager.Users
            .Include(u => u.UserTwoFaMethods)
            .ThenInclude(ut => ut.TwoFaMethod)
            .SingleOrDefaultAsync(u => u.Id == Guid.Parse(selectTwoFaMethodDto.UserId));
        if (user is null)
            throw new AuthorizationException("Select 2FA method");

        if (!user.TwoFactorEnabled)
            throw new TwoFaNotEnabledException();

        var twoFaMethod = user.UserTwoFaMethods.FirstOrDefault(ut => ut.TwoFaMethod.Id == Guid.Parse(selectTwoFaMethodDto.TwoFaMethodId));
        if (twoFaMethod is null)
            throw new TwoFaMethodNotFoundException();

        var selectedTwoFaMethod = user.UserTwoFaMethods.FirstOrDefault(ut => ut.IsSelected);
        if (selectedTwoFaMethod is not null)
            selectedTwoFaMethod.IsSelected = false;

        twoFaMethod.IsSelected = true;

        await _unitOfWork.SaveAsync();

        return new()
        {
            StatusCode = HttpStatusCode.OK,
            Message = $"{EnumHelper.GetEnumDisplayName(twoFaMethod.TwoFaMethod.TwoFactorAuthMethod)} 2FA method successfully selected"
        };
    }
}