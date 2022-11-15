namespace SproutSocial.Application.Features.Commands.Blog.RemoveSavedBlog;

public class RemoveSavedBlogCommandRequest : IRequest<RemoveSavedBlogCommandResponse>
{
    public string BlogId { get; set; } = null!;
}