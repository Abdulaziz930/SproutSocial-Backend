namespace SproutSocial.Application.Features.Queries.Topic.GetTopicById;

public class GetTopicByIdQueryRequest : IRequest<GetTopicByIdQueryResponse>
{
    public string Id { get; set; } = null!;
}
