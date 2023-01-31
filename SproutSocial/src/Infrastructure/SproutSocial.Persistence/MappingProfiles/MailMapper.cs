using AutoMapper;
using SproutSocial.Application.DTOs.MailDtos;
using SproutSocial.Application.Features.Commands.Email.SendEmail;

namespace SproutSocial.Persistence.MappingProfiles;

public class MailMapper : Profile
{
    public MailMapper()
    {
        CreateMap<SendEmailCommandRequest, MailRequestDto>().ReverseMap();
    }
}