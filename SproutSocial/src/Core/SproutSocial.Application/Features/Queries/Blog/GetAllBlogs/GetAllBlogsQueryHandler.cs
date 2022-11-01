namespace SproutSocial.Application.Features.Queries.Blog.GetAllBlogs;

public class GetAllBlogsQueryHandler : IRequestHandler<GetAllBlogsQueryRequest, GetAllBlogsQueryResponse>
{
    private readonly IBlogService _blogService;
    private readonly IMapper _mapper;

    public GetAllBlogsQueryHandler(IBlogService blogService, IMapper mapper)
    {
        _blogService = blogService;
        _mapper = mapper;
    }

    public async Task<GetAllBlogsQueryResponse> Handle(GetAllBlogsQueryRequest request, CancellationToken cancellationToken)
    {
        var blogsDto = await _blogService.GetAllBlogsAsync(request.Page, request.Search);

        var blogs = _mapper.Map<GetAllBlogsQueryResponse>(blogsDto);

        return blogs;
    }
}
