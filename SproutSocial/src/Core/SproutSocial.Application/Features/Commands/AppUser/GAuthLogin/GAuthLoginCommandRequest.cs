namespace SproutSocial.Application.Features.Commands.AppUser.GAuthLogin;

public class GAuthLoginCommandRequest : IRequest<GAuthLoginCommandResponse>
{
    public string Email { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string TwoFaSecurityToken { get; set; } = null!;
}