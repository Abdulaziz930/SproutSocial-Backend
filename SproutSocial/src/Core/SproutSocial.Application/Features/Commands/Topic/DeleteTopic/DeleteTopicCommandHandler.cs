namespace SproutSocial.Application.Features.Commands.Topic.DeleteTopic;

public class DeleteTopicCommandHandler : IRequestHandler<DeleteTopicCommandRequest, DeleteTopicCommandResponse>
{
    private readonly ITopicService _topicService;

    public DeleteTopicCommandHandler(ITopicService topicService)
    {
        _topicService = topicService;
    }

    public async Task<DeleteTopicCommandResponse> Handle(DeleteTopicCommandRequest request, CancellationToken cancellationToken)
    {
        bool result = await _topicService.DeleteTopicAsync(request.Id);

        return new()
        {
            StatusCode = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            Message = result ? "Topic successfully deleted" : "something went wrong"
        };
    }
}
