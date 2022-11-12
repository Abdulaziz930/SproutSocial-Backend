namespace SproutSocial.Application.Features.Commands.Comment.UnLikeComment;

public class UnLikeCommentCommandHandler : IRequestHandler<UnLikeCommentCommandRequest, UnLikeCommentCommandResponse>
{
    private readonly ICommentService _commentService;

    public UnLikeCommentCommandHandler(ICommentService commentService)
    {
        _commentService = commentService;
    }

    public async Task<UnLikeCommentCommandResponse> Handle(UnLikeCommentCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _commentService.UnLikeCommentAsync(request.CommentId);

        return new()
        {
            StatusCode = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            Message = result ? "Comment successfully unliked" : "Something went wrong"
        };
    }
}
