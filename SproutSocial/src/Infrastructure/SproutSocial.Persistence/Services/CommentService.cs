using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SproutSocial.Application.Abstractions.Services;
using SproutSocial.Application.DTOs.BlogDtos;
using SproutSocial.Application.DTOs.CommentDtos;
using SproutSocial.Application.DTOs.Common;
using SproutSocial.Application.Exceptions;
using SproutSocial.Domain.Entities.Identity;

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
            throw new AuthenticationFailException("Please login to comment any post");

        if (string.IsNullOrWhiteSpace(comment.Message))
            throw new ArgumentNullException("Message cannot be empty or null");

        if (string.IsNullOrWhiteSpace(comment.BlogId))
            throw new ArgumentNullException("BlogId cannot be empty or null");

        var dbUser = await _userManager.FindByNameAsync(user.Name);
        if (dbUser is null)
            throw new UserNotFoundException($"User not found by name: {user.Name}");

        var blog = await _unitOfWork.BlogReadRepository.GetSingleAsync(b => !b.IsDeleted && b.Id == Guid.Parse(comment.BlogId), tracking: true, "Comments");
        if (blog is null)
            throw new NotFoundException($"Blog not found by id: {comment.BlogId}");

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
        var comments = _unitOfWork.CommentReadRepository.GetFiltered(c => !c.IsDeleted && c.BlogId == Guid.Parse(blogId), page, 10, tracking: false, "Blog", "AppUser").AsEnumerable();

        var totalCount = await _unitOfWork.CommentReadRepository.GetTotalCountAsync(c => !c.IsDeleted && c.BlogId == Guid.Parse(blogId), "Blog");

        var commetsDto = _mapper.Map<IEnumerable<CommentDto>>(comments);

        PagenatedListDto<CommentDto> pagenatedListDto = new(commetsDto, totalCount, page, 10);

        return pagenatedListDto;
    }

    public async Task<bool> EditCommentAsync(string commentId, UpdateCommentDto updateCommentDto)
    {
        var comment = await _unitOfWork.CommentReadRepository.GetSingleAsync(c => !c.IsDeleted && c.Id == Guid.Parse(commentId));
        if (comment is null)
            throw new NotFoundException($"Comment not found by id: {commentId}");

        comment.Message = updateCommentDto.Message;

        var result = _unitOfWork.CommentWriteRepository.Update(comment);
        await _unitOfWork.SaveAsync();

        return result;
    }

    public async Task<bool> DeleteCommentAsync(string commentId)
    {
        var comment = await _unitOfWork.CommentReadRepository.GetSingleAsync(c => !c.IsDeleted && c.Id == Guid.Parse(commentId));
        if (comment is null)
            throw new NotFoundException($"Comment not found by id: {commentId}");

        comment.IsDeleted = true;

        var result = _unitOfWork.CommentWriteRepository.Update(comment);
        await _unitOfWork.SaveAsync();

        return result;
    }
}
