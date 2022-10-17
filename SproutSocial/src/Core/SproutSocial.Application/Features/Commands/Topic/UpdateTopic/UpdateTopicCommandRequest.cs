namespace SproutSocial.Application.Features.Commands.Topic.UpdateTopic;

public class UpdateTopicCommandRequest : IRequest<UpdateTopicCommandResponse>
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
}
