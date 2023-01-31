namespace SproutSocial.Application.Tests.Features.Queries;

public class GetAllTopicsQueryHandlerTests
{
    private readonly Mock<ITopicService> _topicService;
    private readonly Mock<IBaseUrlAccessor> _baseUrlAccessor;
    private readonly IMapper _mapper;
    private PagenatedListDto<TopicDto> _pagenetedTopics;
    private List<TopicDto> _topics;
    private readonly GetAllTopicsQueryHandler _handler;

    public GetAllTopicsQueryHandlerTests()
    {
        _topics = new List<TopicDto>()
        {
            new(Guid.NewGuid().ToString(), "Topic-1"),
            new(Guid.NewGuid().ToString(), "Topic-2"),
            new(Guid.NewGuid().ToString(), "Topic-3")
        };

        _pagenetedTopics = new(_topics, _topics.Count, 1, 5);

        _topicService = new Mock<ITopicService>();
        _baseUrlAccessor = new Mock<IBaseUrlAccessor>();

        var mapperConfig = new MapperConfiguration(config => config.AddProfile(new TopicMapper()));
        _mapper = mapperConfig.CreateMapper();

        _handler = new GetAllTopicsQueryHandler(_topicService.Object, _mapper);
    }

    [Fact]
    public async Task GetAllTopicsQueryHandler_WhenDataExists_ReturnsListOfTopics()
    {
        _topicService.Setup(x => x.GetAllTopicsAsync(1, null)).ReturnsAsync(_pagenetedTopics);

        GetAllTopicsQueryRequest query = new();

        var result = await _handler.Handle(query, It.IsAny<CancellationToken>());

        Assert.Equal(_topics, result.Topics);
        Assert.Equal(_topics.Count, result.Topics.Count);
    }

    [Fact]
    public async Task GetAllTopicsQueryHandler_WhenDataNoExists_ReturnsEmpty()
    {
        _topicService.Setup(x => x.GetAllTopicsAsync(1, null))
            .ReturnsAsync(new PagenatedListDto<TopicDto>(new List<TopicDto>(), 0, 1, 5));

        GetAllTopicsQueryRequest query = new();

        var result = await _handler.Handle(query, It.IsAny<CancellationToken>());

        Assert.Empty(result.Topics);
    }
}