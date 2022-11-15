namespace SproutSocial.Application.Features.Commands.Blog.SaveBlog;

public class SaveBlogCommandRequest : IRequest<SaveBlogCommandResponse>
{
    public string BlogId { get; set; } = null!;
}