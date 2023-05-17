namespace SproutSocial.Application.Features.Commands.AppUser.TwoFaLogin;

public class TwoFaLoginCommandResponse
{
    public string AccessToken { get; set; } = null!;
    public DateTime Expiration { get; set; }
    public string RefreshToken { get; set; } = null!;
}
