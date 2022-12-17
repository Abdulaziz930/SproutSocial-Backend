namespace SproutSocial.Application.Features.Commands.Subscribe.Unsubscribe;

public sealed class UnsubscribeCommandHandler : IRequestHandler<UnsubscribeCommandRequest, UnsubscribeCommandResponse>
{
    private readonly ISubscribeService _subscribeService;

    public UnsubscribeCommandHandler(ISubscribeService subscribeService)
    {
        _subscribeService = subscribeService;
    }

    public async Task<UnsubscribeCommandResponse> Handle(UnsubscribeCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _subscribeService.UnsubscribeAsync(request.Email);

        return new()
        {
            StatusCode = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            Message = result ? "User successfully unsubscribed" : "Something went wrong"
        };
    }
}