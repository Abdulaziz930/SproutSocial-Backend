using Microsoft.AspNetCore.Http;

namespace SproutSocial.Application.DTOs.BlogDtos;

public record CreateBlogDto
{
    public string Title { get; init; } = null!;
    public string Content { get; init; } = null!;
    public IFormFile FormFile { get; init; } = null!;
    public List<string> TopicIds { get; init; } = null!;
}