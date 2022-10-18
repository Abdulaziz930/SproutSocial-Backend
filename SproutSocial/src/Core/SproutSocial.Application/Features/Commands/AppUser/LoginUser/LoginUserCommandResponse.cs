namespace SproutSocial.Application.Features.Commands.AppUser.LoginUser;

public class LoginUserCommandResponse
{
    public string AccessToken { get; set; } = null!;
    public DateTime Expiration { get; set; }
    public string RefreshToken { get; set; } = null!;
}
