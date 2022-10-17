namespace SproutSocial.Application.Features.Commands.Topic.DeleteTopic;

public class DeleteTopicCommandValidator : AbstractValidator<DeleteTopicCommandRequest>
{
    public DeleteTopicCommandValidator()
    {
        RuleFor(t => t.Id)
            .NotNull().WithMessage("Topic id is required")
            .NotEmpty().WithMessage("Topic id cannot be empty")
            .Must(Validators.IsGuid).WithMessage("Please enter your id correctly");
    }
}
