using SproutSocial.Application.DTOs.UserDtos;

namespace SproutSocial.Application.Features.Commands.AppUser.RefreshTokenLogin;

public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public RefreshTokenLoginCommandHandler(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
    {
        TokenResponseDto tokenResponseDto = await _authService.RefreshTokenLoginAsync(request.RefreshToken);

        var token = _mapper.Map<RefreshTokenLoginCommandResponse>(tokenResponseDto);

        return token;
    }
}
