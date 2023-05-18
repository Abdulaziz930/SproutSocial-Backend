using SproutSocial.Application.DTOs.UserDtos;

namespace SproutSocial.Application.Features.Commands.AppUser.GoogleAuthenticator;

public class SetGAuthCommandHandler : IRequestHandler<SetGAuthCommandRequest, SetGAuthCommandResponse>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public SetGAuthCommandHandler(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<SetGAuthCommandResponse> Handle(SetGAuthCommandRequest request, CancellationToken cancellationToken)
    {
        SetGAuthDto setGAuthDto = _mapper.Map<SetGAuthDto>(request);

        await _authService.SetGAuth(setGAuthDto);

        return new()
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Google authenticator 2FA enabled"
        };
    }
}