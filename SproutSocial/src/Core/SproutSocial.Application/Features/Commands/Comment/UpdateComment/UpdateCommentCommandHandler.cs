using SproutSocial.Application.DTOs.CommentDtos;

namespace SproutSocial.Application.Features.Commands.Comment.UpdateComment;

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommandRequest, UpdateCommentCommandResponse>
{
    private readonly ICommentService _commentService;

    public UpdateCommentCommandHandler(ICommentService commentService)
    {
        _commentService = commentService;
    }

    public async Task<UpdateCommentCommandResponse> Handle(UpdateCommentCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _commentService.EditCommentAsync(request.CommentId, new UpdateCommentDto { Message = request.Message });

        return new()
        {
            StatusCode = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            Message = result ? "Comment successfully updated" : "Something went wrong"
        };
    }
}
