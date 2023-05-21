using AutoMapper;
using SproutSocial.Application.DTOs.UserDtos;
using SproutSocial.Application.Features.Commands.AppUser.SelectTwoFaMethod;

namespace SproutSocial.Persistence.MappingProfiles;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<SelectTwoFaMethodDto, SelectTwoFaMethodCommandRequest>().ReverseMap();
    }
}
