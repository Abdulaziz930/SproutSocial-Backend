namespace SproutSocial.Application.Features.Commands.Blog.UnLikeBlog;

public class UnLikeBlogCommandHandler : IRequestHandler<UnLikeBlogCommandRequest, UnLikeBlogCommandResponse>
{
    private readonly IBlogService _blogService;

    public UnLikeBlogCommandHandler(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public async Task<UnLikeBlogCommandResponse> Handle(UnLikeBlogCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _blogService.UnLikeBlogAsync(request.Id);

        return new()
        {
            StatusCode = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            Message = result ? "Blog successfully unliked" : "Something went wrong"
        };
    }
}
