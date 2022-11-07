namespace SproutSocial.Application.Features.Commands.Comment.DeleteComment;

public class DeleteCommentCommandResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public string? Message { get; set; }
}
