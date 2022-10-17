namespace SproutSocial.Application.Features.Commands.Topic.UpdateTopic;

public class UpdateTopicCommandHandler : IRequestHandler<UpdateTopicCommandRequest, UpdateTopicCommandResponse>
{
    private readonly ITopicService _topicService;
    private readonly IMapper _mapper;

    public UpdateTopicCommandHandler(ITopicService topicService, IMapper mapper)
    {
        _topicService = topicService;
        _mapper = mapper;
    }

    public async Task<UpdateTopicCommandResponse> Handle(UpdateTopicCommandRequest request, CancellationToken cancellationToken)
    {
        var topicDto = _mapper.Map<UpdateTopicDto>(request);

        bool result = await _topicService.UpdateTopicAsync(request.Id, topicDto);
        return new()
        {
            StatusCode = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            Message = result ? "Topic successfully modified" : "something went wrong"
        };
    }
}
