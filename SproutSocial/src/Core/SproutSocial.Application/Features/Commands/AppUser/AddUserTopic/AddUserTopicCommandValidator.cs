namespace SproutSocial.Application.Features.Commands.AppUser.AddUserTopic;

public class AddUserTopicCommandValidator : AbstractValidator<AddUserTopicCommandRequest>
{
    public AddUserTopicCommandValidator()
    {
        RuleFor(u => u.TopicIds)
            .NotNull()
            .Custom((topicIds, context) =>
            {
                if (topicIds.Count == 0)
                    context.AddFailure("TopicIds cannot empty");

                foreach (var topicId in topicIds)
                {
                    if (string.IsNullOrWhiteSpace(topicId))
                        context.AddFailure("TopicId cannot empty");

                    if (!Validators.IsGuid(topicId))
                        context.AddFailure("Please enter correct topicId");
                }
            });
    }
}
