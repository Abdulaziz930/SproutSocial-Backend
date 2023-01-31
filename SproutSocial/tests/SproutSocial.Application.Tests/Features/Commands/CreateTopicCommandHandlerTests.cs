namespace SproutSocial.Application.Tests.Features.Commands;

public class CreateTopicCommandHandlerTests
{
    private readonly Mock<ITopicService> _topicService;
    private readonly Mock<IBaseUrlAccessor> _baseUrlAccessor;
    private readonly IMapper _mapper;
    private readonly CreateTopicCommandHandler _handler;

    public CreateTopicCommandHandlerTests()
    {
        _topicService = new Mock<ITopicService>();
        _baseUrlAccessor = new Mock<IBaseUrlAccessor>();

        var mapperConfig = new MapperConfiguration(config => config.AddProfile(new TopicMapper()));
        _mapper = mapperConfig.CreateMapper();

        _handler = new CreateTopicCommandHandler(_topicService.Object, _mapper);
    }

    [Fact]
    public async Task Handle_GivenValidCommand_CreatesNewTopic()
    {
        CreateTopicDto model = new("New topic");

        _topicService.Setup(x => x.CreateTopicAsync(model)).ReturnsAsync(true);

        CreateTopicCommandRequest command = new()
        {
            Name = model.Name
        };

        var result = await _handler.Handle(command, It.IsAny<CancellationToken>());

        Assert.Equal(HttpStatusCode.Created, result.StatusCode);
    }
}
