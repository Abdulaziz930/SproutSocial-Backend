namespace SproutSocial.Application.Features.Commands.AppUser.GetTwoFaSetup;

public class GetTwoFaSetupCommandHandler : IRequestHandler<GetTwoFaSetupCommandRequest, GetTwoFaSetupCommandResponse>
{
    private readonly IAuthService _authService;

    public GetTwoFaSetupCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<GetTwoFaSetupCommandResponse> Handle(GetTwoFaSetupCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.GetTwoFaSetupAsync(request.Email);

        return new()
        {
            StatusCode = result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            Message = result.IsSuccess ? result.Message : "Something went wrong",
        };
    }
}