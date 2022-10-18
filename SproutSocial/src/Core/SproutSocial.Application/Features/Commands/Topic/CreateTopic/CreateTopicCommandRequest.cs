namespace SproutSocial.Application.Features.Commands.Topic.CreateTopic;

public class CreateTopicCommandRequest : IRequest<CreateTopicCommandResponse>
{
    public string Name { get; set; } = null!;
}
