namespace SproutSocial.Application.Features.Commands.Follow.MakeFollow;

public sealed class MakeFollowCommandValidator : AbstractValidator<MakeFollowCommandRequest>
{
    public MakeFollowCommandValidator()
    {
        RuleFor(f => f.FollowedName)
            .NotNull()
            .WithMessage("Followed username is required")
            .NotEmpty()
            .WithMessage("Followed username cannot be empty")
            .MinimumLength(2)
            .WithMessage("Followed username must not be less than 2 characters")
            .MaximumLength(50)
            .WithMessage("Followed username must not exceed 50 characters");

        RuleFor(f => f.FollowingName)
            .NotNull()
            .WithMessage("Following username is required")
            .NotEmpty()
            .WithMessage("Following username cannot be empty")
            .MinimumLength(2)
            .WithMessage("Following username must not be less than 2 characters")
            .MaximumLength(50)
            .WithMessage("Following username must not exceed 50 characters");
    }
}
