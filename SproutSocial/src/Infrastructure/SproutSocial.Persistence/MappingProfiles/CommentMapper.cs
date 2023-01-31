using AutoMapper;
using SproutSocial.Application.DTOs.BlogDtos;
using SproutSocial.Application.DTOs.CommentDtos;
using SproutSocial.Application.DTOs.Common;
using SproutSocial.Application.DTOs.UserDtos;
using SproutSocial.Application.Features.Commands.Comment.PostComment;
using SproutSocial.Application.Features.Queries.Comment.GetComments;

namespace SproutSocial.Persistence.MappingProfiles;

public class CommentMapper : Profile
{
    public CommentMapper()
    {
        CreateMap<Comment, CommentDto>()
            .ForCtorParam(nameof(CommentDto.UserInfo), from => from.MapFrom(src => new UserInfoDto
            {
                Id = src.AppUser.Id,
                UserName = src.AppUser.UserName
            }))
            .ForCtorParam(nameof(CommentDto.LikeCount), from => from.MapFrom(src => src.CommentLikes.Count()))
            .ForCtorParam(nameof(CommentDto.Likes), from => from.MapFrom(src => src.CommentLikes.Select(x => new CommentLikeDto
            {
                UserName = x.AppUser.UserName,
                UserId = x.AppUser.Id
            })))
            .ReverseMap();
        CreateMap<PostCommentCommandRequest, PostCommentDto>().ReverseMap();
        CreateMap<PagenatedListDto<CommentDto>, GetCommentsQueryResponse>()
            .ForMember(dest => dest.Comments, from => from.MapFrom(src => src.Items)).ReverseMap();
    }
}