namespace SproutSocial.Application.Features.Queries.Comment.GetComments;

public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQueryRequest, GetCommentsQueryResponse>
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;

    public GetCommentsQueryHandler(ICommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }

    public async Task<GetCommentsQueryResponse> Handle(GetCommentsQueryRequest request, CancellationToken cancellationToken)
    {
        var commentsDto = await _commentService.GetComments(request.BlogId, request.Page);

        GetCommentsQueryResponse comments = _mapper.Map<GetCommentsQueryResponse>(commentsDto);

        return comments;
    }
}
