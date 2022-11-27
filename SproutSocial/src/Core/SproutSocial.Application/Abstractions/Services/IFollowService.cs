namespace SproutSocial.Application.Abstractions.Services;

public interface IFollowService
{
    Task<bool> FollowRequestAsync(string followingName, string followedName);
    Task<bool> UnFollowAsync(string followingName, string followedName);
    Task<bool> AcceptOrDeclineFollowRequestAsync(bool acceptOrDeclineFollowRequest, string followedName, string followingName);
}
