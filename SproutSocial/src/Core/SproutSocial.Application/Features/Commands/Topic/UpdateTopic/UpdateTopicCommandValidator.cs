
namespace SproutSocial.Application.Features.Commands.Topic.UpdateTopic;

public class UpdateTopicCommandValidator : AbstractValidator<UpdateTopicCommandRequest>
{
    public UpdateTopicCommandValidator()
    {
        RuleFor(t => t.Id)
            .NotNull().WithMessage("Topic id is required")
            .NotEmpty().WithMessage("Topic id cannot be empty")
            .Must(Validators.IsGuid).WithMessage("Please enter your id correctly");

        RuleFor(t => t.Name)
            .NotNull().WithMessage("Topic name is required")
            .NotEmpty().WithMessage("Topic name cannot be empty")
            .MaximumLength(100).WithMessage("Topic name must not exceed 200 characters");
    }
}
