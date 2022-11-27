namespace SproutSocial.Application.Features.Commands.Follow.AcceptOrDecline;

public sealed class AcceptOrDeclineCommandValidator : AbstractValidator<AcceptOrDeclineCommandRequest>
{
    public AcceptOrDeclineCommandValidator()
    {
        RuleFor(f => f.AcceptOrDeclineFollowRequest)
            .NotNull()
            .WithMessage("Please enter whether you accept or decline the follow request.");

        RuleFor(f => f.FollowedName)
            .NotNull()
            .WithMessage("Followed user name is required")
            .NotEmpty()
            .WithMessage("Followed user name cannot be empty")
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