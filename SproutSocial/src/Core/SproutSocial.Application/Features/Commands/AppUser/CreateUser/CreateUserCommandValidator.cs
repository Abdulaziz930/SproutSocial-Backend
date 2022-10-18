namespace SproutSocial.Application.Features.Commands.AppUser.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommandRequest>
{
    public CreateUserCommandValidator()
    {
        RuleFor(u => u.Fullname)
            .MinimumLength(2)
            .WithMessage("Fullname must not be less than 2 characters")
            .MaximumLength(50)
            .WithMessage("Fullname must not exceed 50 characters");

        RuleFor(u => u.Username)
            .NotNull()
            .WithMessage("Username cannot be null")
            .NotEmpty()
            .WithMessage("Username cannot be empty")
            .MinimumLength(2)
            .WithMessage("Username must not be less than 2 characters")
            .MaximumLength(50)
            .WithMessage("Fullname must not exceed 50 characters");

        RuleFor(u =>u.Email)
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

        RuleFor(u => u.Password)
            .NotNull()
            .WithMessage("Password cannot be null")
            .NotEmpty()
            .WithMessage("Password cannot be empty")
            .MinimumLength(8)
            .WithMessage("Password must not be less than 8 characters")
            .MaximumLength(120)
            .WithMessage("Password must not exceed 120 characters");

        RuleFor(u => u.PasswordConfirm)
            .NotNull()
            .WithMessage("Password cannot be null")
            .NotEmpty()
            .WithMessage("Password cannot be empty")
            .MinimumLength(8)
            .WithMessage("Email address must not be less than 8 characters")
            .MaximumLength(120)
            .WithMessage("Email address must not exceed 120 characters")
            .Equal(u => u.Password)
            .WithMessage("Confirm password must be equal to password");
    }
}
