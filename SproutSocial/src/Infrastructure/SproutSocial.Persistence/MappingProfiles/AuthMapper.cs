using AutoMapper;
using SproutSocial.Application.DTOs.UserDtos;
using SproutSocial.Application.Features.Commands.AppUser.CreateUser;
using SproutSocial.Application.Features.Commands.AppUser.LoginUser;
using SproutSocial.Application.Features.Commands.AppUser.RefreshTokenLogin;

namespace SproutSocial.Persistence.MappingProfiles;

public class AuthMapper : Profile
{
    public AuthMapper()
    {
        CreateMap<CreateUserDto, CreateUserCommandRequest>().ReverseMap();
        CreateMap<TokenResponseDto, RefreshTokenLoginCommandResponse>().ReverseMap();
        CreateMap<LoginDto, LoginUserCommandRequest>().ReverseMap();
    }
}