namespace SproutSocial.Application.Features.Queries.Topic.GetAllTopics;

public class GetAllTopicsQueryHandler : IRequestHandler<GetAllTopicsQueryRequest, GetAllTopicsQueryResponse>
{
    private readonly ITopicService _topicService;

    public GetAllTopicsQueryHandler(ITopicService topicService)
    {
        _topicService = topicService;
    }

    public async Task<GetAllTopicsQueryResponse> Handle(GetAllTopicsQueryRequest request, CancellationToken cancellationToken)
    {
        var topics = await _topicService.GetAllTopicsAsync();

        return new()
        {
            Topics = topics,
        };
    }
}
