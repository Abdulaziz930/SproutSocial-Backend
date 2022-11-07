namespace SproutSocial.Application.Features.Commands.Comment.DeleteComment;

public class DeleteCommentCommandRequest : IRequest<DeleteCommentCommandResponse>
{
    public string CommentId { get; set; } = null!;
}