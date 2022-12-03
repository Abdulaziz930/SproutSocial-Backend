using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SproutSocial.Application.Abstractions.Services;
using SproutSocial.Application.DTOs.BlogDtos;
using SproutSocial.Application.DTOs.CommentDtos;
using SproutSocial.Application.DTOs.Common;
using SproutSocial.Application.Exceptions.Authentication;
using SproutSocial.Application.Exceptions.Blogs;
using SproutSocial.Application.Exceptions.Comments;
using SproutSocial.Application.Exceptions.Users;
using SproutSocial.Domain.Entities.Identity;
using System.Net;

namespace SproutSocial.Persistence.Services;

public class CommentService : ICommentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<AppUser> _userManager;

    public CommentService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public async Task<bool> PostCommentAsync(PostCommentDto comment)
    {
        var user = _httpContextAccessor.HttpContext.User.Identity;
        if (!user.IsAuthenticated)
            throw new AuthorizationException("Please login to comment any post", HttpStatusCode.Unauthorized);

        if (string.IsNullOrWhiteSpace(comment.Message))
            throw new ArgumentNullException("Message cannot be empty or null");

        if (string.IsNullOrWhiteSpace(comment.BlogId))
            throw new ArgumentNullException("BlogId cannot be empty or null");

        var dbUser = await _userManager.FindByNameAsync(user.Name);
        if (dbUser is null)
            throw new UserNotFoundException(nameof(user.Name), user.Name);

        var blog = await _unitOfWork.BlogReadRepository.GetSingleAsync(b => !b.IsDeleted && b.Id == Guid.Parse(comment.BlogId), tracking: true, "Comments");
        if (blog is null)
            throw new BlogNotFoundByIdException(Guid.Parse(comment.BlogId));

        Comment newComment = new()
        {
            Message = comment.Message,
            BlogId = blog.Id,
            AppUserId = dbUser.Id
        };

        var result = await _unitOfWork.CommentWriteRepository.AddAsync(newComment);

        await _unitOfWork.SaveAsync();

        return result;
    }

    public async Task<PagenatedListDto<CommentDto>> GetComments(string blogId, int page)
    {
        var comments = _unitOfWork.CommentReadRepository.GetFiltered(c => !c.IsDeleted && c.BlogId == Guid.Parse(blogId), page, 10, tracking: false, "Blog", "AppUser", "CommentLikes.AppUser").AsEnumerable();

        var totalCount = await _unitOfWork.CommentReadRepository.GetTotalCountAsync(c => !c.IsDeleted && c.BlogId == Guid.Parse(blogId), "Blog");

        var commetsDto = _mapper.Map<IEnumerable<CommentDto>>(comments);

        PagenatedListDto<CommentDto> pagenatedListDto = new(commetsDto, totalCount, page, 10);

        return pagenatedListDto;
    }

    public async Task<bool> EditCommentAsync(string commentId, UpdateCommentDto updateCommentDto)
    {
        var comment = await _unitOfWork.CommentReadRepository.GetSingleAsync(c => !c.IsDeleted && c.Id == Guid.Parse(commentId));
        if (comment is null)
            throw new CommentNotFoundByIdException(Guid.Parse(commentId));

        comment.Message = updateCommentDto.Message;

        var result = _unitOfWork.CommentWriteRepository.Update(comment);
        await _unitOfWork.SaveAsync();

        return result;
    }

    public async Task<bool> DeleteCommentAsync(string commentId)
    {
        var comment = await _unitOfWork.CommentReadRepository.GetSingleAsync(c => !c.IsDeleted && c.Id == Guid.Parse(commentId));
        if (comment is null)
            throw new CommentNotFoundByIdException(Guid.Parse(commentId));

        comment.IsDeleted = true;

        var result = _unitOfWork.CommentWriteRepository.Update(comment);
        await _unitOfWork.SaveAsync();

        return result;
    }

    public async Task<bool> LikeCommentAsync(string commentId)
    {
        ArgumentNullException.ThrowIfNull(commentId);

        var user = _httpContextAccessor.HttpContext.User.Identity;
        if (!user.IsAuthenticated)
            throw new AuthorizationException("Please login to like any comment", HttpStatusCode.Unauthorized);

        var dbUser = await _userManager.FindByNameAsync(user.Name);
        if (dbUser is null)
            throw new UserNotFoundException(nameof(user.Name), user.Name);

        var comment = await _unitOfWork.CommentReadRepository.GetSingleAsync(b => b.Id == Guid.Parse(commentId) && !b.IsDeleted, tracking: true);
        if (comment is null)
            throw new CommentNotFoundByIdException(Guid.Parse(commentId));

        CommentLike commentLike = new()
        {
            CommentId = comment.Id,
            AppUserId = dbUser.Id
        };

        comment.CommentLikes = comment.CommentLikes ?? new List<CommentLike>();
        comment.CommentLikes.Add(commentLike);

        var result = _unitOfWork.CommentWriteRepository.Update(comment);
        await _unitOfWork.SaveAsync();

        return result;
    }

    public async Task<bool> UnLikeCommentAsync(string commentId)
    {
        ArgumentNullException.ThrowIfNull(commentId);

        var user = _httpContextAccessor.HttpContext.User.Identity;
        if (!user.IsAuthenticated)
            throw new AuthorizationException("Please login to unlike any comment", HttpStatusCode.Unauthorized);

        var dbUser = await _userManager.FindByNameAsync(user.Name);
        if (dbUser is null)
            throw new UserNotFoundException(nameof(user.Name), user.Name);

        var comment = await _unitOfWork.CommentReadRepository.GetSingleAsync(b => b.Id == Guid.Parse(commentId) && !b.IsDeleted, tracking: true, "CommentLikes");
        if (comment is null)
            throw new CommentNotFoundByIdException(Guid.Parse(commentId));

        var likedBlog = comment.CommentLikes.FirstOrDefault(b => b.CommentId == Guid.Parse(commentId) && b.AppUserId == dbUser.Id);

        comment.CommentLikes.Remove(likedBlog);

        var result = _unitOfWork.CommentWriteRepository.Update(comment);
        await _unitOfWork.SaveAsync();

        return result;
    }
}
