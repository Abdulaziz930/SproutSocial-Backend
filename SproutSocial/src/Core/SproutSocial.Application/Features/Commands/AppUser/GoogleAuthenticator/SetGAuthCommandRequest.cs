namespace SproutSocial.Application.Features.Commands.AppUser.GoogleAuthenticator;

public class SetGAuthCommandRequest : IRequest<SetGAuthCommandResponse>
{
    public string Email { get; set; } = null!;
    public string Code { get; set; } = null!;
}