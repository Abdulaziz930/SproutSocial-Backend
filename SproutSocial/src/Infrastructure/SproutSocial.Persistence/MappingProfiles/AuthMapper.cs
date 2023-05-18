using AutoMapper;
using SproutSocial.Application.DTOs.UserDtos;
using SproutSocial.Application.Features.Commands.AppUser.ConfirmEmail;
using SproutSocial.Application.Features.Commands.AppUser.CreateUser;
using SproutSocial.Application.Features.Commands.AppUser.EnableTwoFa;
using SproutSocial.Application.Features.Commands.AppUser.GAuthLogin;
using SproutSocial.Application.Features.Commands.AppUser.GoogleAuthenticator;
using SproutSocial.Application.Features.Commands.AppUser.LoginUser;
using SproutSocial.Application.Features.Commands.AppUser.RefreshTokenLogin;
using SproutSocial.Application.Features.Commands.AppUser.TwoFaLogin;

namespace SproutSocial.Persistence.MappingProfiles;

public class AuthMapper : Profile
{
    public AuthMapper()
    {
        CreateMap<CreateUserDto, CreateUserCommandRequest>().ReverseMap();
        CreateMap<TokenResponseDto, RefreshTokenLoginCommandResponse>().ReverseMap();
        CreateMap<LoginDto, LoginUserCommandRequest>().ReverseMap();
        CreateMap<ConfirmEmailDto, ConfirmEmailCommandRequest>().ReverseMap();
        CreateMap<TwoFaLoginDto, TwoFaLoginCommandRequest>().ReverseMap();
        CreateMap<SetGAuthDto, SetGAuthCommandRequest>().ReverseMap();
        CreateMap<TwoFaLoginDto, GAuthLoginCommandRequest>().ReverseMap();
        CreateMap<EnableTwoFaDto, EnableTwoFaCommandRequest>().ReverseMap();
    }
}