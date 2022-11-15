namespace SproutSocial.Application.Features.Commands.Blog.SaveBlog;

public class SaveBlogCommandHandler : IRequestHandler<SaveBlogCommandRequest, SaveBlogCommandResponse>
{
    private readonly IUserService _userService;

    public SaveBlogCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<SaveBlogCommandResponse> Handle(SaveBlogCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.SaveBlogAsync(request.BlogId);

        return new()
        {
            StatusCode = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            Message = result ? "Blog successfully saved" : "Something went wrong"
        };
    }
}
