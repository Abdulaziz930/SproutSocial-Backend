using Microsoft.AspNetCore.Http;

namespace SproutSocial.Application.DTOs.MailDtos;

public class MailRequestDto
{
    public string ToEmail { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
    public List<IFormFile>? Attachments { get; set; }
}