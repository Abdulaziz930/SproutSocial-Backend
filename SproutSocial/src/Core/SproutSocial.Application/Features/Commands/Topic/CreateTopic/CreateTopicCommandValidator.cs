namespace SproutSocial.Application.Features.Commands.Product.CreateTopic;

public class CreateTopicCommandValidator : AbstractValidator<CreateTopicCommandRequest>
{
    public CreateTopicCommandValidator()
    {
        RuleFor(t => t.Name)
            .NotNull().WithMessage("Topic name is required")
            .NotEmpty().WithMessage("Topic name cannot be empty")
            .MaximumLength(100).WithMessage("Topic name must not exceed 200 characters");
    }
}
