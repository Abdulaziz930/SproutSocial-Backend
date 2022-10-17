namespace SproutSocial.Application.Features.Commands.Topic.DeleteTopic;

public class DeleteTopicCommandRequest : IRequest<DeleteTopicCommandResponse>
{
    public string Id { get; set; } = null!;
}
