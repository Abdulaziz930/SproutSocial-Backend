using SproutSocial.Application.DTOs.UserDtos;

namespace SproutSocial.Application.Features.Commands.AppUser.EnableTwoFa;

public class EnableTwoFaCommandHandler : IRequestHandler<EnableTwoFaCommandRequest, EnableTwoFaCommandResponse>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public EnableTwoFaCommandHandler(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<EnableTwoFaCommandResponse> Handle(EnableTwoFaCommandRequest request, CancellationToken cancellationToken)
    {
        EnableTwoFaDto enableTwoFaDto = _mapper.Map<EnableTwoFaDto>(request);

        await _authService.EnableTwoFaAsync(enableTwoFaDto);

        return new()
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Google authenticator 2FA enabled"
        };
    }
}
