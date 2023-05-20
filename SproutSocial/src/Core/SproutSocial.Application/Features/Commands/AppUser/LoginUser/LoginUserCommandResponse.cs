using SproutSocial.Domain.Enums;

namespace SproutSocial.Application.Features.Commands.AppUser.LoginUser;

public class LoginUserCommandResponse
{
    public bool RequiresTwoFactor { get; set; }
    public TwoFactorAuthMethod TwoFactorAuthMethod { get; set; }
    public string? TwoFaSecurityToken { get; set; }
    public LoginUserCommandTokenResponse? TokenResponse { get; set; }
}

public class LoginUserCommandTokenResponse
{
    public string AccessToken { get; set; } = null!;
    public DateTime Expiration { get; set; }
    public string RefreshToken { get; set; } = null!;
}