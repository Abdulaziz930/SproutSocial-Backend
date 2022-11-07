namespace SproutSocial.Application.Features.Commands.Comment.DeleteComment;

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommandRequest, DeleteCommentCommandResponse>
{
    private readonly ICommentService _commentService;

    public DeleteCommentCommandHandler(ICommentService commentService)
    {
        _commentService = commentService;
    }

    public async Task<DeleteCommentCommandResponse> Handle(DeleteCommentCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _commentService.DeleteCommentAsync(request.CommentId);

        return new()
        {
            StatusCode = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            Message = result ? "Comment successfully deleted" : "Something went wrong"
        };
    }
}
