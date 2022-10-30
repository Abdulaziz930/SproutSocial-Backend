namespace SproutSocial.Application.Features.Commands.Blog.UpdateBlog;

public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommandRequest, UpdateBlogCommandResponse>
{
    private readonly IBlogService _blogService;
    private readonly IMapper _mapper;

    public UpdateBlogCommandHandler(IBlogService blogService, IMapper mapper)
    {
        _blogService = blogService;
        _mapper = mapper;
    }

    public async Task<UpdateBlogCommandResponse> Handle(UpdateBlogCommandRequest request, CancellationToken cancellationToken)
    {
        UpdateBlogDto topicDto = _mapper.Map<UpdateBlogDto>(request);

        bool result = await _blogService.UpdateBlogAsync(request.Id, topicDto);

        return new()
        {
            StatusCode = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            Message = result ? "Blog successfully modified" : "something went wrong"
        };
    }
}
