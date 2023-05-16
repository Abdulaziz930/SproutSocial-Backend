namespace SproutSocial.Application.Features.Commands.AppUser.ConfirmEmail;

public class ConfirmEmailCommandRequest : IRequest<ConfirmEmailCommandResponse>
{
    public string? Token { get; set; }
    public string? Email { get; set; }
}