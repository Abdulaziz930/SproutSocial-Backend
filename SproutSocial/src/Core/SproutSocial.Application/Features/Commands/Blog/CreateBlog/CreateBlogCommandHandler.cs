namespace SproutSocial.Application.Features.Commands.Blog.CreateBlog;

public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommandRequest, CreateBlogCommandResponse>
{
    private readonly IBlogService _blogService;

    public CreateBlogCommandHandler(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public async Task<CreateBlogCommandResponse> Handle(CreateBlogCommandRequest request, CancellationToken cancellationToken)
    {
        bool result = await _blogService.CreateBlogAsync(new CreateBlogDto
        {
            Title = request.Title,
            Content = request.Content,
            FormFile = request.FormFile,
            TopicIds = request.TopicIds,
        });

        return new()
        {
            StatusCode = result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
            Message = result ? "Topic successfully created" : "something went wrong"
        };
    }
}
