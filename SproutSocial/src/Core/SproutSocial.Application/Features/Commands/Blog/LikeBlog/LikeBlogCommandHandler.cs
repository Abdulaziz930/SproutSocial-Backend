namespace SproutSocial.Application.Features.Commands.Blog.LikeBlog;

public class LikeBlogCommandHandler : IRequestHandler<LikeBlogCommandRequest, LikeBlogCommandResponse>
{
    private readonly IBlogService _blogService;

    public LikeBlogCommandHandler(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public async Task<LikeBlogCommandResponse> Handle(LikeBlogCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _blogService.LikeBlogAsync(request.Id);

        return new()
        {
            StatusCode = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            Message = result ? "Blog successfully liked" : "Something went wrong"
        };
    }
}
