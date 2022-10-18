using System.ComponentModel.DataAnnotations;

namespace SproutSocial.Application.Features.Commands.AppUser.LoginUser;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommandRequest>
{
    public LoginUserCommandValidator()
    {
        RuleFor(l => l.UsernameOrEmail)
            .NotNull()
            .WithMessage("Username and email address cannot be null")
            .NotEmpty()
            .WithMessage("Username and email addres s cannot be empty")
            .Custom((e, context) =>
            {
                if (e.Contains("@"))
                {
                    bool isValid = new EmailAddressAttribute().IsValid(e);
                    if (!isValid)
                    {
                        context.AddFailure("Please enter valid email address");
                    }
                }
            });

        RuleFor(l => l.Password)
            .NotNull()
            .WithMessage("Password cannot be null")
            .NotEmpty()
            .WithMessage("Password cannot be empty")
            .MinimumLength(8)
            .WithMessage("Password must not be less than 8 characters")
            .MaximumLength(120)
            .WithMessage("Password must not exceed 120 characters");
    }
}
