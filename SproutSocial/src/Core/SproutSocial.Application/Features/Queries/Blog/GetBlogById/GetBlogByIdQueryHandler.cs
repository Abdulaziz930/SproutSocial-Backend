namespace SproutSocial.Application.Features.Queries.Blog.GetBlogById;

public class GetBlogByIdQueryHandler : IRequestHandler<GetBlogByIdQueryRequest, GetBlogByIdQueryResponse>
{
    private readonly IBlogService _blogService;
    private readonly IMapper _mapper;

    public GetBlogByIdQueryHandler(IBlogService blogService, IMapper mapper)
    {
        _blogService = blogService;
        _mapper = mapper;
    }

    public async Task<GetBlogByIdQueryResponse> Handle(GetBlogByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var blogDto = await _blogService.GetBlogByIdAsync(request.Id);

        var blog = _mapper.Map<GetBlogByIdQueryResponse>(blogDto);

        return blog;
    }
}
