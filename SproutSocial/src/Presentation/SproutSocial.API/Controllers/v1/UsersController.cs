using SproutSocial.Application.Features.Commands.AppUser.CreateUser;
using SproutSocial.Application.Features.Commands.AppUser.LoginUser;
using SproutSocial.Application.Features.Commands.AppUser.RefreshTokenLogin;

namespace SproutSocial.API.Controllers.v1;

[ApiVersion("1")]
public class UsersController : BaseController
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserCommandRequest createUserCommandRequest)
    {
        var response = await _mediator.Send(createUserCommandRequest);

        return StatusCode((int)response.StatusCode, response.Message);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
    {
        var response = await _mediator.Send(loginUserCommandRequest);

        return Ok(response);
    }

    [HttpPost("refresh-token-login")]
    public async Task<IActionResult> RefreshTokenLogin(RefreshTokenLoginCommandRequest refreshTokenLoginCommandRequest)
    {
        var response = await _mediator.Send(refreshTokenLoginCommandRequest);

        return Ok(response);
    }
}
