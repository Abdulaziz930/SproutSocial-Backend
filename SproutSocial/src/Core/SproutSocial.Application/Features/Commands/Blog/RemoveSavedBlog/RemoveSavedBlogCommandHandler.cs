namespace SproutSocial.Application.Features.Commands.Blog.RemoveSavedBlog;

public class RemoveSavedBlogCommandHandler : IRequestHandler<RemoveSavedBlogCommandRequest, RemoveSavedBlogCommandResponse>
{
    private readonly IUserService _userService;

    public RemoveSavedBlogCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<RemoveSavedBlogCommandResponse> Handle(RemoveSavedBlogCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.RemoveSavedBlogAsync(request.BlogId);

        return new()
        {
            StatusCode = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            Message = result ? "Saved blog successfully removed" : "Something went wrong"
        };
    }
}
