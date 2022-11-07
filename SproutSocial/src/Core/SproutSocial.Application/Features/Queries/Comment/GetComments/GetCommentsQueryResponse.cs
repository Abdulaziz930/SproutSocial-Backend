using SproutSocial.Application.DTOs.CommentDtos;

namespace SproutSocial.Application.Features.Queries.Comment.GetComments;

public class GetCommentsQueryResponse
{
    public List<CommentDto> Comments { get; init; } = null!;
    public int PageIndex { get; init; }
    public int TotalPages { get; init; }
    public bool HasNext { get; init; }
    public bool HasPrev { get; init; }
}
