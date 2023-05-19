using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using SproutSocial.Application.Features.Commands.AppUser.AddUserTopic;
using SproutSocial.Application.Features.Commands.AppUser.ConfirmEmail;
using SproutSocial.Application.Features.Commands.AppUser.CreateUser;
using SproutSocial.Application.Features.Commands.AppUser.EnableTwoFa;
using SproutSocial.Application.Features.Commands.AppUser.GAuthLogin;
using SproutSocial.Application.Features.Commands.AppUser.GetTwoFaSetup;
using SproutSocial.Application.Features.Commands.AppUser.GoogleAuthenticator;
using SproutSocial.Application.Features.Commands.AppUser.LoginUser;
using SproutSocial.Application.Features.Commands.AppUser.RefreshTokenLogin;
using SproutSocial.Application.Features.Commands.AppUser.TwoFaLogin;
using SproutSocial.Application.Features.Commands.Blog.RemoveSavedBlog;
using SproutSocial.Application.Features.Commands.Blog.SaveBlog;
using SproutSocial.Application.Features.Commands.Follow.AcceptOrDecline;
using SproutSocial.Application.Features.Commands.Follow.MakeFollow;
using SproutSocial.Application.Features.Commands.Follow.UnFollow;
using SproutSocial.Application.Features.Queries.GoogleAuthenticator;

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

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailCommandRequest confirmEmailCommandRequest)
    {
        var response = await _mediator.Send(confirmEmailCommandRequest);

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

    [Authorize]
    [HttpGet("TwoFaSetup/{Email}")]
    public async Task<IActionResult> TwoFaSetup([FromRoute] GetTwoFaSetupCommandRequest getTwoFaSetupCommandRequest)
    {
        var response = await _mediator.Send(getTwoFaSetupCommandRequest);

        return StatusCode((int)response.StatusCode, response.Message);
    }

    [Authorize]
    [HttpPost("EnableTwoFa")]
    public async Task<IActionResult> EnableTwoFa(EnableTwoFaCommandRequest enableTwoFaCommandRequest)
    {
        var response = await _mediator.Send(enableTwoFaCommandRequest);

        return StatusCode((int)response.StatusCode, response.Message);
    }

    [HttpPost("TwoFaLogin")]
    public async Task<IActionResult> TwoFaLogin(TwoFaLoginCommandRequest twoFaLoginCommandRequest)
    {
        var response = await _mediator.Send(twoFaLoginCommandRequest);

        return Ok(response);
    }

    [Authorize]
    [HttpGet("GAuthSetup/{Email}")]
    public async Task<IActionResult> GAuthSetup([FromRoute] GAuthSetupQueryRequest gAuthSetupQueryRequest)
    {
        var response = await _mediator.Send(gAuthSetupQueryRequest);

        return File(response.QrCode, "image/png");
    }

    [Authorize]
    [HttpPost("EnableGAuth")]
    public async Task<IActionResult> EnableGAuth(SetGAuthCommandRequest setGAuthCommandRequest)
    {
        var response = await _mediator.Send(setGAuthCommandRequest);

        return StatusCode((int)response.StatusCode, response.Message);
    }

    [HttpPost("GAuthLogin")]
    public async Task<IActionResult> GAuthLogin(GAuthLoginCommandRequest gAuthLoginCommandRequest)
    {
        var response = await _mediator.Send(gAuthLoginCommandRequest);

        return Ok(response);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("add-topics-to-user")]
    public async Task<IActionResult> AddTopicUser(AddUserTopicCommandRequest addUserTopicCommand)
    {
        var response = await _mediator.Send(addUserTopicCommand);

        return StatusCode((int)response.StatusCode, response?.Message);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("save-blog/{blogId}")]
    public async Task<IActionResult> SaveBlog([FromRoute] SaveBlogCommandRequest saveBlogCommandRequest)
    {
        var response = await _mediator.Send(saveBlogCommandRequest);

        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("remove-saved-blog/{blogId}")]
    public async Task<IActionResult> RemoveSavedBlog([FromRoute] RemoveSavedBlogCommandRequest removeSavedBlogCommandRequest)
    {
        var response = await _mediator.Send(removeSavedBlogCommandRequest);

        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("sent-follow-request")]
    public async Task<IActionResult> SentFollowRequest([FromBody] MakeFollowCommandRequest makeFollowCommandRequest)
    {
        var response = await _mediator.Send(makeFollowCommandRequest);

        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("accept-or-decline-follow-request")]
    public async Task<IActionResult> AcceptOrDeclineFollowRequest([FromBody] AcceptOrDeclineCommandRequest 
            acceptOrDeclineCommandRequest)
    {
        var response = await _mediator.Send(acceptOrDeclineCommandRequest);

        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("unfollow")]
    public async Task<IActionResult> UnFollow([FromBody] UnFollowCommandRequest unFollowCommandRequest)
    {
        var response = await _mediator.Send(unFollowCommandRequest);

        return StatusCode((int)response.StatusCode, response);
    }
}