namespace SproutSocial.Application.Features.Commands.Product.CreateTopic;

public class CreateTopicCommandRequest : IRequest<CreateTopicCommandResponse>
{
    public string Name { get; set; } = null!;
}
