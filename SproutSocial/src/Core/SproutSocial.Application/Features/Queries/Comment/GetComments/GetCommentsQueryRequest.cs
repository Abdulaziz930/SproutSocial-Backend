namespace SproutSocial.Application.Features.Queries.Comment.GetComments;

public class GetCommentsQueryRequest : IRequest<GetCommentsQueryResponse>
{
    public string BlogId { get; set; } = null!;
    public int Page { get; set; } = 1;
}