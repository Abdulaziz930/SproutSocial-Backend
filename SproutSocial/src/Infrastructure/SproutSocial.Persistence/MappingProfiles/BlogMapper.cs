using AutoMapper;
using SproutSocial.Application.Abstractions.Common;
using SproutSocial.Application.DTOs.BlogDtos;
using SproutSocial.Application.DTOs.Common;
using SproutSocial.Application.DTOs.UserDtos;
using SproutSocial.Application.Features.Commands.Blog.UpdateBlog;
using SproutSocial.Application.Features.Queries.Blog.GetAllBlogs;
using SproutSocial.Application.Features.Queries.Blog.GetBlogById;

namespace SproutSocial.Persistence.MappingProfiles;

public class BlogMapper : Profile
{
    private readonly IBaseUrlAccessor _baseUrlAccessor;

    public BlogMapper(IBaseUrlAccessor baseUrlAccessor)
    {
        _baseUrlAccessor = baseUrlAccessor;

        CreateMap<Blog, BlogDto>()
            .ForCtorParam(nameof(BlogDto.Image), from => from.MapFrom(src => $"{_baseUrlAccessor.BaseUrl}/{src.BlogImage.Path}"))
            .ForCtorParam(nameof(BlogDto.Topics), from => from.MapFrom(src => src.BlogTopics.Select(x => x.Topic).ToList()))
            .ForCtorParam(nameof(BlogDto.LikeCount), from => from.MapFrom(src => src.BlogLikes.Count()))
            .ForCtorParam(nameof(BlogDto.Likes), from => from.MapFrom(src => src.BlogLikes.Select(x => new BlogLikeDto
            {
                UserName = x.AppUser.UserName,
                UserId = x.AppUser.Id
            })))
            .ForCtorParam(nameof(BlogDto.UserInfo), from => from.MapFrom(src => new UserInfoDto
            {
                Id = src.AppUser.Id,
                UserName = src.AppUser.UserName
            }))
            .ReverseMap();
        CreateMap<BlogDto, GetAllBlogsQueryResponse>().ReverseMap();
        CreateMap<BlogDto, GetBlogByIdQueryResponse>().ReverseMap();
        CreateMap<UpdateBlogDto, UpdateBlogCommandRequest>().ReverseMap();
        CreateMap<PagenatedListDto<BlogDto>, GetAllBlogsQueryResponse>()
            .ForMember(dest => dest.Blogs, from => from.MapFrom(src => src.Items)).ReverseMap();
    }
}