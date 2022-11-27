namespace SproutSocial.Application.Features.Commands.Follow.AcceptOrDecline;

public sealed class AcceptOrDeclineCommandHandler : IRequestHandler<AcceptOrDeclineCommandRequest, AcceptOrDeclineCommandResponse>
{
    private readonly IFollowService _followService;

    public AcceptOrDeclineCommandHandler(IFollowService followService)
    {
        _followService = followService;
    }

    public async Task<AcceptOrDeclineCommandResponse> Handle(AcceptOrDeclineCommandRequest request, CancellationToken cancellationToken)
    {
        bool result = await _followService.AcceptOrDeclineFollowRequestAsync(request.AcceptOrDeclineFollowRequest, request.FollowedName, request.FollowingName);

        return new()
        {
            StatusCode = HttpStatusCode.OK,
            Message = result ? "Follow request accepted" : "Follow request not accepted"
        };
    }
}