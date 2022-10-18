namespace SproutSocial.Application.Features.Commands.AppUser.RefreshTokenLogin;

public class RefreshTokenLoginCommandValidator : AbstractValidator<RefreshTokenLoginCommandRequest>
{
    public RefreshTokenLoginCommandValidator()
    {
        RuleFor(r => r.RefreshToken)
            .NotNull()
            .WithMessage("Refresh token cannot be null")
            .NotEmpty()
            .WithMessage("Refresh token cannot be empty");
    }
}
