namespace SproutSocial.Application.Features.Commands.AppUser.GAuthLogin;

public class GAuthLoginCommandResponse
{
    public string AccessToken { get; set; } = null!;
    public DateTime Expiration { get; set; }
    public string RefreshToken { get; set; } = null!;
}
