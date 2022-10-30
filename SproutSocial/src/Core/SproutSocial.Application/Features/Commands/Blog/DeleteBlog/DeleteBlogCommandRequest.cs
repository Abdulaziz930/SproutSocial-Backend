namespace SproutSocial.Application.Features.Commands.Blog.DeleteBlog;

public class DeleteBlogCommandRequest : IRequest<DeleteBlogCommandResponse>
{
    public string Id { get; set; } = null!;
}