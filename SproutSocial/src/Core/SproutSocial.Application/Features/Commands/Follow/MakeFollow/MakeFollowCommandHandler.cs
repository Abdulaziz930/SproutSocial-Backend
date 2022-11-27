namespace SproutSocial.Application.Features.Commands.Follow.MakeFollow;

public sealed class MakeFollowCommandHandler : IRequestHandler<MakeFollowCommandRequest, MakeFollowCommandResponse>
{
    private readonly IFollowService _followService;

    public MakeFollowCommandHandler(IFollowService followService)
    {
        _followService = followService;
    }

    public async Task<MakeFollowCommandResponse> Handle(MakeFollowCommandRequest request, CancellationToken cancellationToken)
    {
        bool result = await _followService.FollowRequestAsync(request.FollowingName, request.FollowedName);

        return new()
        {
            StatusCode = result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
            Message = result ? "Follow request successfully sended" : "Something went wrong"
        };
    }
}