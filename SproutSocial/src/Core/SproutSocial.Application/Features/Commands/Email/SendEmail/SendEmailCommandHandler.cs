using SproutSocial.Application.Abstractions.Email;
using SproutSocial.Application.DTOs.MailDtos;

namespace SproutSocial.Application.Features.Commands.Email.SendEmail;

public class SendEmailCommandHandler : IRequestHandler<SendEmailCommandRequest, SendEmailCommandResponse>
{
    private readonly IMailService _mailService;
    private readonly IMapper _mapper;

    public SendEmailCommandHandler(IMailService mailService, IMapper mapper)
    {
        _mailService = mailService;
        _mapper = mapper;
    }

    public async Task<SendEmailCommandResponse> Handle(SendEmailCommandRequest request, CancellationToken cancellationToken)
    {
        var mailRequesDto = _mapper.Map<MailRequestDto>(request);

        await _mailService.SendEmailAsync(mailRequesDto);

        return new()
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Mail successfully sent"
        };
    }
}