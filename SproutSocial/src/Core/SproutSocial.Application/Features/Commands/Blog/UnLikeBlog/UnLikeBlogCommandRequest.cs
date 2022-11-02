namespace SproutSocial.Application.Features.Commands.Blog.UnLikeBlog;

public class UnLikeBlogCommandRequest : IRequest<UnLikeBlogCommandResponse>
{
    public string Id { get; set; } = null!;
}