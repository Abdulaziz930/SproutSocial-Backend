namespace SproutSocial.Application.Features.Commands.Topic.CreateTopic;

public class CreateTopicCommandHandler : IRequestHandler<CreateTopicCommandRequest, CreateTopicCommandResponse>
{
    private readonly ITopicService _topicService;
    private readonly IMapper _mapper;

    public CreateTopicCommandHandler(ITopicService topicService, IMapper mapper)
    {
        _topicService = topicService;
        _mapper = mapper;
    }

    public async Task<CreateTopicCommandResponse> Handle(CreateTopicCommandRequest request, CancellationToken cancellationToken)
    {
        var topicDto = _mapper.Map<CreateTopicDto>(request);

        bool result = await _topicService.CreateTopicAsync(topicDto);

        return new()
        {
            StatusCode = result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
            Message = result ? "Topic successfully created" : "something went wrong"
        };
    }
}
