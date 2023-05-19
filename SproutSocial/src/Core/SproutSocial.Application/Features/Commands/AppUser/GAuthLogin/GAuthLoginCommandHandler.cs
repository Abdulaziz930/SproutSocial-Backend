using SproutSocial.Application.DTOs.UserDtos;

namespace SproutSocial.Application.Features.Commands.AppUser.GAuthLogin;

public class GAuthLoginCommandHandler : IRequestHandler<GAuthLoginCommandRequest, GAuthLoginCommandResponse>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public GAuthLoginCommandHandler(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<GAuthLoginCommandResponse> Handle(GAuthLoginCommandRequest request, CancellationToken cancellationToken)
    {
        TwoFaLoginDto twoFaLoginDto = _mapper.Map<TwoFaLoginDto>(request);

        var result = await _authService.GAuthLoginAsync(twoFaLoginDto, 2);

        return new()
        {
            AccessToken = result.AccessToken,
            Expiration = result.Expiration,
            RefreshToken = result.RefreshToken
        };
    }
}
