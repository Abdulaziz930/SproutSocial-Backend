namespace SproutSocial.Application.Features.Commands.AppUser.GetTwoFaSetup;

public class GetTwoFaSetupCommandRequest : IRequest<GetTwoFaSetupCommandResponse>
{
    public string Email { get; set; } = null!;
}