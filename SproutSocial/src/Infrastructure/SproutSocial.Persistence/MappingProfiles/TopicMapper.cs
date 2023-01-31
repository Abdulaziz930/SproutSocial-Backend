using AutoMapper;
using SproutSocial.Application.DTOs.Common;
using SproutSocial.Application.DTOs.TopicDtos;
using SproutSocial.Application.Features.Commands.Topic.CreateTopic;
using SproutSocial.Application.Features.Commands.Topic.UpdateTopic;
using SproutSocial.Application.Features.Queries.Topic.GetAllTopics;

namespace SproutSocial.Persistence.MappingProfiles;

public class TopicMapper : Profile
{
    public TopicMapper()
    {
        CreateMap<Topic, CreateTopicDto>().ReverseMap();
        CreateMap<Topic, TopicDto>().ReverseMap();
        CreateMap<PagenatedListDto<TopicDto>, GetAllTopicsQueryResponse>()
            .ForMember(dest => dest.Topics, from => from.MapFrom(src => src.Items)).ReverseMap();

        CreateMap<CreateTopicDto, CreateTopicCommandRequest>().ReverseMap();
        CreateMap<UpdateTopicDto, UpdateTopicCommandRequest>().ReverseMap();
    }
}