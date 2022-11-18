namespace SproutSocial.Application.Features.Commands.AppUser.FollowUser;

public class FollowUserCommandHandler : IRequestHandler<FollowUserCommandRequest, FollowUserCommandResponse>
{
    private readonly IUserService _userService;

    public FollowUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<FollowUserCommandResponse> Handle(FollowUserCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.FollowRequestAsync(request.UserId);

        return new()
        {
            StatusCode = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            Message = result ? "Follow request successfully sended" : "Something went wrong"
        };
    }
}
