using AutoMapper;
using SproutSocial.Core.Entities;
using SproutSocial.Service.Dtos.BlogDtos;
using SproutSocial.Service.Dtos.TopicDtos;
using SproutSocial.Service.Dtos.UserTopicDtos;
using SproutSocial.Service.HelperServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Service.Profiles
{
    public class AutoMapperProfile : Profile
    {
        private readonly IHelperAccessor _helperAccessor;

        public AutoMapperProfile(IHelperAccessor helperAccessor)
        {
            _helperAccessor = helperAccessor;

            CreateMap<Topic, TopicDetailDto>().ReverseMap();
            CreateMap<Topic, TopicListItemDto>().ReverseMap();
            CreateMap<Topic, TopicPostDto>().ReverseMap();

            CreateMap<UserTopic, UserTopicDto>().ReverseMap();

            CreateMap<BlogPostDto, Blog>().ForMember(dto => dto.BlogTopics, opt => opt.Ignore()).ReverseMap();
            CreateMap<Blog, BlogDetailDto>()
                .ForMember(dest => dest.FilePath, from => from.MapFrom(src => $"{_helperAccessor.BaseUrl}/uploads/blogs/{src.Image}"))
                .ReverseMap();
            CreateMap<Blog, BlogListItemDto>()
                .ForMember(dest => dest.FilePath, from => from.MapFrom(src => $"{_helperAccessor.BaseUrl}/uploads/blogs/{src.Image}"))
                .ReverseMap();
        }
    }
}
