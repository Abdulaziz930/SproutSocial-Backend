using AutoMapper;
using SproutSocial.Application.DTOs.TopicDtos;
using SproutSocial.Application.DTOs.UserDtos;
using SproutSocial.Application.Features.Commands.AppUser.CreateUser;
using SproutSocial.Application.Features.Commands.AppUser.LoginUser;
using SproutSocial.Application.Features.Commands.AppUser.RefreshTokenLogin;
using SproutSocial.Application.Features.Commands.Topic.CreateTopic;
using SproutSocial.Application.Features.Commands.Topic.UpdateTopic;

namespace SproutSocial.Persistence.MappingProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Topic, CreateTopicDto>().ReverseMap();
        CreateMap<Topic, TopicDto>().ReverseMap();

        CreateMap<CreateTopicDto, CreateTopicCommandRequest>().ReverseMap();
        CreateMap<UpdateTopicDto, UpdateTopicCommandRequest>().ReverseMap();

        CreateMap<CreateUserDto, CreateUserCommandRequest>().ReverseMap();
        CreateMap<TokenResponseDto, RefreshTokenLoginCommandResponse>().ReverseMap();
        CreateMap<LoginDto, LoginUserCommandRequest>().ReverseMap();
    }
}
