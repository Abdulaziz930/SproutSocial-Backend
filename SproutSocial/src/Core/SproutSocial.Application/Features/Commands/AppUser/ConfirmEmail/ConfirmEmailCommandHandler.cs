using SproutSocial.Application.DTOs.UserDtos;

namespace SproutSocial.Application.Features.Commands.AppUser.ConfirmEmail;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommandRequest, ConfirmEmailCommandResponse>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public ConfirmEmailCommandHandler(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<ConfirmEmailCommandResponse> Handle(ConfirmEmailCommandRequest request, CancellationToken cancellationToken)
    {
        ConfirmEmailDto confirmEmailDto = _mapper.Map<ConfirmEmailDto>(request);

        var result = await _authService.ConfirmEmailAsync(confirmEmailDto);

        return new()
        {
            StatusCode = result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            Message = result.IsSuccess ? result.Message : "Something went wrong",
        };
    }
}
