namespace SproutSocial.Application.Features.Commands.Follow.AcceptOrDecline;

public sealed class AcceptOrDeclineCommandRequest : IRequest<AcceptOrDeclineCommandResponse>
{
    public bool AcceptOrDeclineFollowRequest { get; set; }
    public string FollowedName { get; set; } = null!;
    public string FollowingName { get; set; } = null!;
}