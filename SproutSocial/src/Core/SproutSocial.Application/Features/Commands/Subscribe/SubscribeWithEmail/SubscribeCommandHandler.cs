namespace SproutSocial.Application.Features.Commands.Subscribe.SubscribeWithEmail;

public sealed class SubscribeCommandHandler : IRequestHandler<SubscribeCommandRequest, SubscribeCommandResponse>
{
    private readonly ISubscribeService _subscribeService;

    public SubscribeCommandHandler(ISubscribeService subscribeService)
    {
        _subscribeService = subscribeService;
    }

    public async Task<SubscribeCommandResponse> Handle(SubscribeCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _subscribeService.SubscribeAsync(request.Email);

        return new()
        {
            StatusCode = result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
            Message = result ? "User successfully subscribed" : "Something went wrong"
        };
    }
}