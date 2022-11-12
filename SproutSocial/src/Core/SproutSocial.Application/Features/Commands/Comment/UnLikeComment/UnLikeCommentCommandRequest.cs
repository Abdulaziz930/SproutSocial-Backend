namespace SproutSocial.Application.Features.Commands.Comment.UnLikeComment;

public class UnLikeCommentCommandRequest : IRequest<UnLikeCommentCommandResponse>
{
    public string CommentId { get; set; } = null!;
}