namespace SproutSocial.Application.Features.Commands.Comment.PostComment;

public class PostCommentCommandRequest : IRequest<PostCommentCommandResponse>
{
    public string Message { get; set; } = null!;
    public string BlogId { get; set; } = null!;
    public string? CommentId { get; set; }
}