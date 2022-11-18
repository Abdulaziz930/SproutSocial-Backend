using Microsoft.AspNetCore.Http;

namespace SproutSocial.Application.DTOs.BlogDtos;

public record UpdateBlogDto(
    string Title, 
    string Content, 
    IFormFile? FormFile, 
    List<string>? TopicIds
);