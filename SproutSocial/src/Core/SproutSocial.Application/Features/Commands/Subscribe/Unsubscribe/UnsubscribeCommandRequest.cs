namespace SproutSocial.Application.Features.Commands.Subscribe.Unsubscribe;

public sealed class UnsubscribeCommandRequest : IRequest<UnsubscribeCommandResponse>
{
    public string Email { get; set; } = null!;
}