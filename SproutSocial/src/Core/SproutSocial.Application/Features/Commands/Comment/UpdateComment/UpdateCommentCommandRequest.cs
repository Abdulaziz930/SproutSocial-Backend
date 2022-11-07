namespace SproutSocial.Application.Features.Commands.Comment.UpdateComment;

public class UpdateCommentCommandRequest : IRequest<UpdateCommentCommandResponse>
{
    public string CommentId { get; set; } = null!;
    public string Message { get; set; } = null!;
}