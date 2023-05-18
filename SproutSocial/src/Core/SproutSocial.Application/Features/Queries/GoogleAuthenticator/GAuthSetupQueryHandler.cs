namespace SproutSocial.Application.Features.Queries.GoogleAuthenticator;

public class GAuthSetupQueryHandler : IRequestHandler<GAuthSetupQueryRequest, GAuthSetupQueryResponse>
{
    private readonly IAuthService _authService;

    public GAuthSetupQueryHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<GAuthSetupQueryResponse> Handle(GAuthSetupQueryRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.GetGAuthSetup(request.Email);

        return new()
        {
            QrCode = result
        };
    }
}