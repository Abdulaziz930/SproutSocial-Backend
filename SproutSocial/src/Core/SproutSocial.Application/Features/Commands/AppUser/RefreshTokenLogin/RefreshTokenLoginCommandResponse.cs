namespace SproutSocial.Application.Features.Commands.AppUser.RefreshTokenLogin;

public class RefreshTokenLoginCommandResponse
{
    public string AccessToken { get; set; } = null!;
    public DateTime Expiration { get; set; }
    public string RefreshToken { get; set; } = null!;
}
