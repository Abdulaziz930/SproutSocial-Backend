namespace SproutSocial.Application.Features.Queries.Topic.GetAllTopics;

public class GetAllTopicsQueryHandler : IRequestHandler<GetAllTopicsQueryRequest, GetAllTopicsQueryResponse>
{
    private readonly ITopicService _topicService;
    private readonly IMapper _mapper;

    public GetAllTopicsQueryHandler(ITopicService topicService, IMapper mapper)
    {
        _topicService = topicService;
        _mapper = mapper;
    }

    public async Task<GetAllTopicsQueryResponse> Handle(GetAllTopicsQueryRequest request, CancellationToken cancellationToken)
    {
        var topicsDto = await _topicService.GetAllTopicsAsync(request.Page, request.Search);

        var topics = _mapper.Map<GetAllTopicsQueryResponse>(topicsDto);

        return topics;
    }
}
