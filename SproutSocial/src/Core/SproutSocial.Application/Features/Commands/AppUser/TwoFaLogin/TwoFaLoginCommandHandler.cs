using SproutSocial.Application.DTOs.UserDtos;

namespace SproutSocial.Application.Features.Commands.AppUser.TwoFaLogin;

public class TwoFaLoginCommandHandler : IRequestHandler<TwoFaLoginCommandRequest, TwoFaLoginCommandResponse>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public TwoFaLoginCommandHandler(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<TwoFaLoginCommandResponse> Handle(TwoFaLoginCommandRequest request, CancellationToken cancellationToken)
    {
        TwoFaLoginDto twoFaLoginDto = _mapper.Map<TwoFaLoginDto>(request);

        var result = await _authService.TwoFaLoginAsync(twoFaLoginDto, 2);

        return new()
        {
            AccessToken = result.AccessToken,
            Expiration = result.Expiration,
            RefreshToken = result.RefreshToken,
        };
    }
}
