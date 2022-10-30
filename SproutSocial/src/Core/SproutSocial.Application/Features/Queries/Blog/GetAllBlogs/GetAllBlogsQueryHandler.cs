namespace SproutSocial.Application.Features.Queries.Blog.GetAllBlogs;

public class GetAllBlogsQueryHandler : IRequestHandler<GetAllBlogsQueryRequest, List<GetAllBlogsQueryResponse>>
{
    private readonly IBlogService _blogService;
    private readonly IMapper _mapper;

    public GetAllBlogsQueryHandler(IBlogService blogService, IMapper mapper)
    {
        _blogService = blogService;
        _mapper = mapper;
    }

    public async Task<List<GetAllBlogsQueryResponse>> Handle(GetAllBlogsQueryRequest request, CancellationToken cancellationToken)
    {
        var blogsDto = await _blogService.GetAllBlogsAsync();

        var blogs = _mapper.Map<List<GetAllBlogsQueryResponse>>(blogsDto);

        return blogs;
    }
}
