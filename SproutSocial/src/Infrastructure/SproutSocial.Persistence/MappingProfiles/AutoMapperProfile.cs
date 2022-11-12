using AutoMapper;
using SproutSocial.Application.Abstractions.Common;
using SproutSocial.Application.DTOs.BlogDtos;
using SproutSocial.Application.DTOs.CommentDtos;
using SproutSocial.Application.DTOs.Common;
using SproutSocial.Application.DTOs.TopicDtos;
using SproutSocial.Application.DTOs.UserDtos;
using SproutSocial.Application.Features.Commands.AppUser.CreateUser;
using SproutSocial.Application.Features.Commands.AppUser.LoginUser;
using SproutSocial.Application.Features.Commands.AppUser.RefreshTokenLogin;
using SproutSocial.Application.Features.Commands.Blog.UpdateBlog;
using SproutSocial.Application.Features.Commands.Comment.PostComment;
using SproutSocial.Application.Features.Commands.Topic.CreateTopic;
using SproutSocial.Application.Features.Commands.Topic.UpdateTopic;
using SproutSocial.Application.Features.Queries.Blog.GetAllBlogs;
using SproutSocial.Application.Features.Queries.Blog.GetBlogById;
using SproutSocial.Application.Features.Queries.Comment.GetComments;
using SproutSocial.Application.Features.Queries.Topic.GetAllTopics;

namespace SproutSocial.Persistence.MappingProfiles;

public class AutoMapperProfile : Profile
{
    private readonly IBaseUrlAccessor _baseUrlAccessor;

    public AutoMapperProfile(IBaseUrlAccessor baseUrlAccessor)
    {
        _baseUrlAccessor = baseUrlAccessor;

        CreateMap<Topic, CreateTopicDto>().ReverseMap();
        CreateMap<Topic, TopicDto>().ReverseMap();
        CreateMap<PagenatedListDto<TopicDto>, GetAllTopicsQueryResponse>()
            .ForMember(dest => dest.Topics, from => from.MapFrom(src => src.Items)).ReverseMap();

        CreateMap<CreateTopicDto, CreateTopicCommandRequest>().ReverseMap();
        CreateMap<UpdateTopicDto, UpdateTopicCommandRequest>().ReverseMap();

        CreateMap<CreateUserDto, CreateUserCommandRequest>().ReverseMap();
        CreateMap<TokenResponseDto, RefreshTokenLoginCommandResponse>().ReverseMap();
        CreateMap<LoginDto, LoginUserCommandRequest>().ReverseMap();

        CreateMap<Blog, BlogDto>()
            .ForMember(dest => dest.Image, from => from.MapFrom(src => $"{_baseUrlAccessor.BaseUrl}/{src.BlogImage.Path}"))
            .ForMember(dest => dest.Topics, from => from.MapFrom(src => src.BlogTopics.Select(x => x.Topic).ToList()))
            .ForPath(dest => dest.UserInfo.Id, from => from.MapFrom(src => src.AppUser.Id))
            .ForPath(dest => dest.UserInfo.UserName, from => from.MapFrom(src => src.AppUser.UserName))
            .ForMember(dest => dest.LikeCount, from => from.MapFrom(src => src.BlogLikes.Count()))
            .ForPath(dest => dest.Likes, from => from.MapFrom(src => src.BlogLikes.Select(x => new BlogLikeDto
            {
                UserName = x.AppUser.UserName,
                UserId = x.AppUser.Id
            })))
            .ReverseMap();
        CreateMap<BlogDto, GetAllBlogsQueryResponse>().ReverseMap();
        CreateMap<BlogDto, GetBlogByIdQueryResponse>().ReverseMap();
        CreateMap<UpdateBlogDto, UpdateBlogCommandRequest>().ReverseMap();
        CreateMap<PagenatedListDto<BlogDto>, GetAllBlogsQueryResponse>()
            .ForMember(dest => dest.Blogs, from => from.MapFrom(src => src.Items)).ReverseMap();

        CreateMap<Comment, CommentDto>()
            .ForPath(dest => dest.UserInfo.Id, from => from.MapFrom(src => src.AppUser.Id))
            .ForPath(dest => dest.UserInfo.UserName, from => from.MapFrom(src => src.AppUser.UserName))
            .ReverseMap();
        CreateMap<PostCommentCommandRequest, PostCommentDto>().ReverseMap();
        CreateMap<PagenatedListDto<CommentDto>, GetCommentsQueryResponse>()
            .ForMember(dest => dest.Comments, from => from.MapFrom(src => src.Items)).ReverseMap();
    }
}
