using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using SproutSocial.Application.Features.Commands.Subscribe.SubscribeWithEmail;
using SproutSocial.Application.Features.Commands.Subscribe.Unsubscribe;

namespace SproutSocial.API.Controllers.v1;

[ApiVersion("1")]
[Authorize]
public class SubscribesController : BaseController
{
    private readonly IMediator _mediator;

    public SubscribesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Subscribe([FromBody] SubscribeCommandRequest subscribeCommandRequest)
    {
        var response = await _mediator.Send(subscribeCommandRequest);

        return StatusCode((int)response.StatusCode, response.Message);
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> Unsubscribe([FromBody] UnsubscribeCommandRequest unsubscribeCommandRequest)
    {
        var response = await _mediator.Send(unsubscribeCommandRequest);

        return StatusCode((int)response.StatusCode, response.Message);
    }
}