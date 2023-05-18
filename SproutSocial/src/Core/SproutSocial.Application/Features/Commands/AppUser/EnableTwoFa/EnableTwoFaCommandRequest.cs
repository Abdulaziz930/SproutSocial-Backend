namespace SproutSocial.Application.Features.Commands.AppUser.EnableTwoFa;

public class EnableTwoFaCommandRequest : IRequest<EnableTwoFaCommandResponse>
{
    public string Email { get; set; } = null!;
    public string Code { get; set; } = null!;
}