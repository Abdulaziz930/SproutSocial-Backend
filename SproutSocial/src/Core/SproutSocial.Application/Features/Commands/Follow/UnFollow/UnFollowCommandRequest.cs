namespace SproutSocial.Application.Features.Commands.Follow.UnFollow;

public sealed class UnFollowCommandRequest : IRequest<UnFollowCommandResponse>
{
    public string FollowedName { get; set; } = null!;
    public string FollowingName { get; set; } = null!;
}