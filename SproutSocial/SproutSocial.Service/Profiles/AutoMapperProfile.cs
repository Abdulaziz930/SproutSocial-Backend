using AutoMapper;
using SproutSocial.Core.Entities;
using SproutSocial.Service.Dtos.TopicDtos;
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
        }
    }
}
