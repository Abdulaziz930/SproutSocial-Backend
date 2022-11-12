namespace SproutSocial.Application.Features.Commands.Comment.LikeComment;

public class LikeCommentCommandRequest : IRequest<LikeCommentCommandResponse>
{
    public string CommentId { get; set; } = null!;
}