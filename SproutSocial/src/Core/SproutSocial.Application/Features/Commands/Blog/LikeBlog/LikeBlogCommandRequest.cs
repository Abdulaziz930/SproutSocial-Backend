namespace SproutSocial.Application.Features.Commands.Blog.LikeBlog;

public class LikeBlogCommandRequest : IRequest<LikeBlogCommandResponse>
{
    public string Id { get; set; } = null!;
}