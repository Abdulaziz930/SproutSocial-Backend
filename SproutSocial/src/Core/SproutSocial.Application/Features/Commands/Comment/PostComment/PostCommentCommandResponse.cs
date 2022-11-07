namespace SproutSocial.Application.Features.Commands.Comment.PostComment;

public class PostCommentCommandResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public string? Message { get; set; }
}
