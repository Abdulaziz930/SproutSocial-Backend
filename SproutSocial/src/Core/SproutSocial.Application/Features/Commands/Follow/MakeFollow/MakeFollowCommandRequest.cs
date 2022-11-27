namespace SproutSocial.Application.Features.Commands.Follow.MakeFollow;

public sealed class MakeFollowCommandRequest : IRequest<MakeFollowCommandResponse>
{
    public string FollowedName { get; set; } = null!;
    public string FollowingName { get; set; } = null!;
}