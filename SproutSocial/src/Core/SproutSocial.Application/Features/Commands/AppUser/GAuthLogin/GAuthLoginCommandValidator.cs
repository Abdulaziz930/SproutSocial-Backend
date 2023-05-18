using System.ComponentModel.DataAnnotations;

namespace SproutSocial.Application.Features.Commands.AppUser.GAuthLogin;

public class GAuthLoginCommandValidator : AbstractValidator<GAuthLoginCommandRequest>
{
    public GAuthLoginCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotNull()
            .WithMessage("2FA Code cannot be null")
            .NotEmpty()
            .WithMessage("2FA Code cannot be null")
            .Length(6)
            .WithMessage("2FA Code should be 6 character")
            .Custom((e, context) =>
            {
                if (!Validators.IsNumber(e))
                    context.AddFailure("Please enter valid 2fa code");
            });

        RuleFor(x => x.Email)
            .NotNull()
            .WithMessage("Email address cannot be null")
            .NotEmpty()
            .WithMessage("Email address cannot be empty")
            .Custom((e, context) =>
            {
                bool isValid = new EmailAddressAttribute().IsValid(e);
                if (!isValid)
                {
                    context.AddFailure("Please enter valid email address");
                }
            });
    }
}