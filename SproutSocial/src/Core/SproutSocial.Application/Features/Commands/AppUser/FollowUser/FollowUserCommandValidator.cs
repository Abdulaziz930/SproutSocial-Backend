namespace SproutSocial.Application.Features.Commands.AppUser.FollowUser;

public class FollowUserCommandValidator : AbstractValidator<FollowUserCommandRequest>
{
    public FollowUserCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotNull().WithMessage("User id is required")
            .NotEmpty().WithMessage("User id cannot be empty")
            .Custom((id, context) =>
            {
                if (!Validators.IsGuid(id))
                    context.AddFailure("Please enter correct user id");
            });
    }
}