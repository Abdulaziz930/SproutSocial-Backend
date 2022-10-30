using Microsoft.AspNetCore.Http;

namespace SproutSocial.Application.Features.Commands.Blog.CreateBlog;

public class CreateBlogCommandRequest : IRequest<CreateBlogCommandResponse>
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public IFormFile FormFile { get; set; } = null!;
    public List<string> TopicIds { get; set; } = null!;
}