using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SproutSocial.Application.Abstractions.Services;
using SproutSocial.Application.DTOs.UserDtos;
using SproutSocial.Application.Exceptions;
using SproutSocial.Domain.Entities.Identity;
using SproutSocial.Persistence.Enums;

namespace SproutSocial.Persistence.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
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

        throw new UserNotFoundException("User not authenticated");
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
            return new()
            {
                IsSuccess = true,
                Message = "User successfully created"
            };
        }

        throw new UserCreateFailedException();
    }

    public async Task<bool> SaveBlogAsync(string blogId)
    {
        ArgumentNullException.ThrowIfNull(blogId);

        var user = _httpContextAccessor.HttpContext.User.Identity;
        if (!user.IsAuthenticated)
            throw new AuthenticationFailException("Please login to save any post");

        var dbUser = await _userManager.FindByNameAsync(user.Name);
        if (dbUser is null)
            throw new UserNotFoundException($"User not found by name: {user.Name}");

        var blog = await _unitOfWork.BlogReadRepository.GetSingleAsync(b => b.Id == Guid.Parse(blogId) && !b.IsDeleted, tracking: false);
        if (blog is null)
            throw new NotFoundException($"Blog not found with id: {blogId}");

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
            throw new AuthenticationFailException("Please login to remove saved post");

        var dbUser = await _userManager.FindByNameAsync(user.Name);
        if (dbUser is null)
            throw new UserNotFoundException($"User not found by name: {user.Name}");

        var blog = await _unitOfWork.BlogReadRepository.GetSingleAsync(b => b.Id == Guid.Parse(blogId) && !b.IsDeleted, tracking: true, "SavedBlogs");
        if (blog is null)
            throw new NotFoundException($"Blog not found with id: {blogId}");

        var savedBlog = blog.SavedBlogs.FirstOrDefault(b => b.BlogId == Guid.Parse(blogId) && b.AppUserId == dbUser.Id);
        if (savedBlog is null)
            throw new NotFoundException("Saved blog not found");

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
}
