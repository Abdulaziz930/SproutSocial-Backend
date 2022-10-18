using SproutSocial.Application.DTOs.UserDtos;

namespace SproutSocial.Application.Features.Commands.AppUser.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    public LoginUserCommandHandler(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }
    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        LoginDto loginDto = _mapper.Map<LoginDto>(request);

        var result = await _authService.LoginAsync(loginDto, 2);

        return new()
        {
            AccessToken = result.AccessToken,
            Expiration = result.Expiration,
            RefreshToken = result.RefreshToken
        };
    }
}
