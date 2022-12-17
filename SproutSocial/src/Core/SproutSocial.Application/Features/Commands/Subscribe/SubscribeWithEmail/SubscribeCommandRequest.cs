namespace SproutSocial.Application.Features.Commands.Subscribe.SubscribeWithEmail;

public sealed class SubscribeCommandRequest : IRequest<SubscribeCommandResponse>
{
    public string Email { get; set; } = null!;
}