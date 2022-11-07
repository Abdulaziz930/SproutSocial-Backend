namespace SproutSocial.Application.Features.Commands.Comment.PostComment;

public class PostCommentCommandHandler : IRequestHandler<PostCommentCommandRequest, PostCommentCommandResponse>
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;

    public PostCommentCommandHandler(ICommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }

    public async Task<PostCommentCommandResponse> Handle(PostCommentCommandRequest request, CancellationToken cancellationToken)
    {
        var commentDto = _mapper.Map<PostCommentDto>(request);

        var result = await _commentService.PostCommentAsync(commentDto);

        return new()
        {
            StatusCode = result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
            Message = result ? "Comment successfully created" : "Something went wrong"
        };
    }
}
