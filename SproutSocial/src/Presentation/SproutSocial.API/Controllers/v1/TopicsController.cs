using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using SproutSocial.Application.Features.Commands.Topic.CreateTopic;
using SproutSocial.Application.Features.Commands.Topic.DeleteTopic;
using SproutSocial.Application.Features.Commands.Topic.UpdateTopic;
using SproutSocial.Application.Features.Queries.Topic.GetAllTopics;
using SproutSocial.Application.Features.Queries.Topic.GetTopicById;

namespace SproutSocial.API.Controllers;

[ApiVersion("1")]
[Authorize]
public class TopicsController : BaseController
{
    private readonly IMediator _mediator;

    public TopicsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll([FromQuery] GetAllTopicsQueryRequest getAllTopicsQueryRequest)
    {
        return Ok(await _mediator.Send(getAllTopicsQueryRequest));
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(string id)
    {
        var response = await _mediator.Send(new GetTopicByIdQueryRequest
        { 
            Id = id
        });
        return Ok(response.Topic);
    }

    [HttpPost("")]
    public async Task<IActionResult> Create(CreateTopicCommandRequest topic)
    {
        var response = await _mediator.Send(topic);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut("")]
    public async Task<IActionResult> Update(UpdateTopicCommandRequest topic)
    {
        var response = await _mediator.Send(topic);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("")]
    public async Task<IActionResult> Delete(DeleteTopicCommandRequest topic)
    {
        var response = await _mediator.Send(topic);
        return StatusCode((int)response.StatusCode, response);
    }
}
