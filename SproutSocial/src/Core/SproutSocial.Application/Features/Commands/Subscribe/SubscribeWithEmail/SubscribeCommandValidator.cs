namespace SproutSocial.Application.Features.Commands.Subscribe.SubscribeWithEmail;

public sealed class SubscribeCommandValidator : AbstractValidator<SubscribeCommandRequest>
{
    public SubscribeCommandValidator()
    {
        RuleFor(s => s.Email)
            .NotNull()
            .WithMessage("Email address cannot be null")
            .NotEmpty()
            .WithMessage("Email address cannot be empty")
            .MinimumLength(4)
            .WithMessage("Email address must not be less than 4 characters")
            .MaximumLength(256)
            .WithMessage("Email address must not exceed 256 characters")
            .EmailAddress()
            .WithMessage("Please enter valid email address");
    }
}