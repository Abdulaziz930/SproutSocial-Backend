using SproutSocial.Application.DTOs.UserDtos;

namespace SproutSocial.Application.Features.Commands.AppUser.SelectTwoFaMethod;

public class SelectTwoFaMethodCommandHandler : IRequestHandler<SelectTwoFaMethodCommandRequest, SelectTwoFaMethodCommandResponse>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public SelectTwoFaMethodCommandHandler(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<SelectTwoFaMethodCommandResponse> Handle(SelectTwoFaMethodCommandRequest request, CancellationToken cancellationToken)
    {
        SelectTwoFaMethodDto selectTwoFaMethodDto = _mapper.Map<SelectTwoFaMethodDto>(request);

        var result = await _userService.SelectTwoFaMethodAsync(selectTwoFaMethodDto);

        return new()
        {
            StatusCode = result.StatusCode,
            Message = result.Message
        };
    }
}