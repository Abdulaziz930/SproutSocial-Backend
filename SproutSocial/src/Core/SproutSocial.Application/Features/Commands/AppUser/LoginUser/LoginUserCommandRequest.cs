namespace SproutSocial.Application.Features.Commands.AppUser.LoginUser;

public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
{
    public string UsernameOrEmail { get; set; } = null!;
    public string Password { get; set; } = null!;
}