using SproutSocial.Application.Features.Commands.Email.SendEmail;

namespace SproutSocial.API.Controllers.v1
{
    [ApiVersion("1")]
    public class MailController : BaseController
    {
        private readonly IMediator _mediator;

        public MailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("")]
        public async Task<IActionResult> SendMail([FromForm] SendEmailCommandRequest sendEmailCommandRequest)
        {
            var response = await _mediator.Send(sendEmailCommandRequest);
            return StatusCode((int)response.StatusCode, response.Message);
        }
    }
}