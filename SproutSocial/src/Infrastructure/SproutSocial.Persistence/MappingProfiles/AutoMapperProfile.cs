using AutoMapper;
using SproutSocial.Application.DTOs.TopicDtos;
using SproutSocial.Application.Features.Commands.Product.CreateTopic;
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
    }
}
