namespace SproutSocial.Application.Features.Commands.AppUser.SelectTwoFaMethod;

public class SelectTwoFaMethodCommandValidator : AbstractValidator<SelectTwoFaMethodCommandRequest>
{
    public SelectTwoFaMethodCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("User id is required")
            .NotEmpty().WithMessage("User id cannot be empty")
            .Custom((id, context) =>
            {
                if (!Validators.IsGuid(id))
                    context.AddFailure("Please enter correct User id");
            });

        RuleFor(x => x.TwoFaMethodId)
            .NotNull().WithMessage("2FA method id is required")
            .NotEmpty().WithMessage("2FA method id cannot be empty")
            .Custom((id, context) =>
            {
                if (!Validators.IsGuid(id))
                    context.AddFailure("Please enter correct 2FA method id");
            });
    }
}