namespace SproutSocial.Application.Features.Commands.Follow.UnFollow;

public sealed class UnFollowCommandHandler : IRequestHandler<UnFollowCommandRequest, UnFollowCommandResponse>
{
    private readonly IFollowService _followService;

    public UnFollowCommandHandler(IFollowService followService)
    {
        _followService = followService;
    }

    public async Task<UnFollowCommandResponse> Handle(UnFollowCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _followService.UnFollowAsync(request.FollowingName, request.FollowedName);

        return new()
        {
            StatusCode = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            Message = result ? "User successfully unfollowed" : "Something went wrong"
        };
    }
}