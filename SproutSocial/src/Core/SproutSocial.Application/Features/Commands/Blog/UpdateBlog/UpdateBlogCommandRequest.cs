using Microsoft.AspNetCore.Http;

namespace SproutSocial.Application.Features.Commands.Blog.UpdateBlog;

public class UpdateBlogCommandRequest : IRequest<UpdateBlogCommandResponse>
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public IFormFile? FormFile { get; set; }
    public List<string>? TopicIds { get; set; }
}