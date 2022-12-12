namespace SproutSocial.Application.Features.Queries.Topic.GetAllTopics;

public class GetAllTopicsQueryRequest : IRequest<GetAllTopicsQueryResponse>
{
    public int Page { get; set; } = 1;
    public string? Search { get; set; }
}
