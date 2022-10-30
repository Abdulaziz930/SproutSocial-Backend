namespace SproutSocial.Application.Features.Commands.Blog.DeleteBlog;

public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommandRequest, DeleteBlogCommandResponse>
{
    private readonly IBlogService _blogService;

    public DeleteBlogCommandHandler(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public async Task<DeleteBlogCommandResponse> Handle(DeleteBlogCommandRequest request, CancellationToken cancellationToken)
    {
        bool result = await _blogService.DeleteBlogAsync(request.Id);

        return new()
        {
            StatusCode = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            Message = result ? "Blog successfully deleted" : "Something went wrong"
        };
    }
}
