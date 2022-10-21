namespace SproutSocial.Application.Features.Commands.AppUser.AddUserTopic;

public class AddUserTopicCommandRequest : IRequest<AddUserTopicCommandResponse>
{
    public List<string> TopicIds { get; set; } = null!;
}