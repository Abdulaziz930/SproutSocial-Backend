using Microsoft.AspNetCore.Http;

namespace SproutSocial.Application.DTOs.BlogDtos;

public class UpdateBlogDto
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public IFormFile? FormFile { get; set; }
    public List<string>? TopicIds { get; set; }
}
