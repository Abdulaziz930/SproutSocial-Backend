using SproutSocial.Application.DTOs.UserDtos;

namespace SproutSocial.Application.Features.Commands.AppUser.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        CreateUserDto createUserDto = _mapper.Map<CreateUserDto>(request);

        var result = await _userService.CreateAsync(createUserDto);

        return new()
        {
            StatusCode = result.IsSuccess ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
            Message = result.IsSuccess ? result.Message : "Somthing went wrong",
        };
    }
}
