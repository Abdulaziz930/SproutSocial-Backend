using Microsoft.AspNetCore.Http;

namespace SproutSocial.Application.Features.Commands.Email.SendEmail;

public class SendEmailCommandRequest : IRequest<SendEmailCommandResponse>
{
    public string ToEmail { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
    public List<IFormFile>? Attachments { get; set; }
}