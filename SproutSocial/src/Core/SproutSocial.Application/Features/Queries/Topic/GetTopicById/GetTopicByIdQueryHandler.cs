namespace SproutSocial.Application.Features.Queries.Topic.GetTopicById;

public class GetTopicByIdQueryHandler : IRequestHandler<GetTopicByIdQueryRequest, GetTopicByIdQueryResponse>
{
    private readonly ITopicService _topicService;

    public GetTopicByIdQueryHandler(ITopicService topicService)
    {
        _topicService = topicService;
    }

    public async Task<GetTopicByIdQueryResponse> Handle(GetTopicByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var topic = await _topicService.GetTopicByIdAsync(request.Id);

        return new()
        {
            Topic = topic
        };
    }
}
