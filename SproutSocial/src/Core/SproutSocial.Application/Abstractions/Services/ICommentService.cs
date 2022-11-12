using SproutSocial.Application.DTOs.CommentDtos;
using SproutSocial.Application.DTOs.Common;

namespace SproutSocial.Application.Abstractions.Services;

public interface ICommentService
{
    Task<PagenatedListDto<CommentDto>> GetComments(string blogId, int page);
    Task<bool> PostCommentAsync(PostCommentDto comment);
    Task<bool> EditCommentAsync(string commentId, UpdateCommentDto updateCommentDto);
    Task<bool> DeleteCommentAsync(string commentId);
    Task<bool> LikeCommentAsync(string commentId);
    Task<bool> UnLikeCommentAsync(string commentId);
}
