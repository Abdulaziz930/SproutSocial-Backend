namespace SproutSocial.Application.Features.Queries.GetTopicById;

public class GetTopicByIdQueryRequest : IRequest<GetTopicByIdQueryResponse>
{
    public string Id { get; set; } = null!;
}
