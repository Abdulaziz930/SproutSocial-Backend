namespace SproutSocial.Application.Features.Commands.AppUser.FollowUser;

public class FollowUserCommandRequest : IRequest<FollowUserCommandResponse>
{
    public string UserId { get; set; } = null!;
}