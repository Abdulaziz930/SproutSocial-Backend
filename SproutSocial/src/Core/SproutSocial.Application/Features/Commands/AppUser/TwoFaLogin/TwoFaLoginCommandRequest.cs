namespace SproutSocial.Application.Features.Commands.AppUser.TwoFaLogin;

public class TwoFaLoginCommandRequest : IRequest<TwoFaLoginCommandResponse>
{
    public string Code { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string TwoFaSecurityToken { get; set; } = null!;
}