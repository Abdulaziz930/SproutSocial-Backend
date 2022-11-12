namespace SproutSocial.Application.Features.Commands.Comment.LikeComment;

public class LikeCommentCommandHandler : IRequestHandler<LikeCommentCommandRequest, LikeCommentCommandResponse>
{
    private readonly ICommentService _commentService;

    public LikeCommentCommandHandler(ICommentService commentService)
    {
        _commentService = commentService;
    }

    public async Task<LikeCommentCommandResponse> Handle(LikeCommentCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _commentService.LikeCommentAsync(request.CommentId);

        return new()
        {
            StatusCode = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            Message = result ? "Comment successfully liked" : "Something went wrong"
        };
    }
}
