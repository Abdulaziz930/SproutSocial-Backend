namespace SproutSocial.Application.Features.Commands.Blog.DeleteBlog;

public class DeleteBlogCommandResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public string? Message { get; set; }
}
