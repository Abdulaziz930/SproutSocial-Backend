using SproutSocial.Application.DTOs.MailDtos;

namespace SproutSocial.Application.Abstractions.Email;

public interface IMailService
{
    Task SendEmailAsync(MailRequestDto mailRequest);
}