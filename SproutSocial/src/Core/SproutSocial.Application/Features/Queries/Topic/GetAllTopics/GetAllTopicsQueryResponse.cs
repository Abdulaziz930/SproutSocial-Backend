namespace SproutSocial.Application.Features.Queries.Topic.GetAllTopics;

public class GetAllTopicsQueryResponse
{
    public List<TopicDto> Topics { get; set; } = null!;
    public int PageIndex { get; init; }
    public int TotalPages { get; init; }
    public bool HasNext { get; init; }
    public bool HasPrev { get; init; }
}
