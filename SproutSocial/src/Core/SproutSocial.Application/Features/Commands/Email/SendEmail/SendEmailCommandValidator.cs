namespace SproutSocial.Application.Features.Commands.Email.SendEmail;

public class SendEmailCommandValidator : AbstractValidator<SendEmailCommandRequest>
{
    public SendEmailCommandValidator()
    {
        RuleFor(e => e.ToEmail)
            .NotNull().WithMessage("Email address cannot be null")
            .NotEmpty().WithMessage("Email address cannot be empty")
            .MinimumLength(2).WithMessage("Email address must not be less than 2 characters")
            .MaximumLength(80).WithMessage("Email address must not exceed 80 characters");

        RuleFor(e => e.Subject)
            .NotNull().WithMessage("Subject cannot be null")
            .NotEmpty().WithMessage("Subject cannot be empty")
            .MinimumLength(1).WithMessage("Subject must not be less than 1 characters")
            .MaximumLength(150).WithMessage("Subject must not exceed 150 characters");

        RuleFor(e => e.Body)
            .NotNull().WithMessage("Body cannot be null")
            .NotEmpty().WithMessage("Body cannot be empty")
            .MinimumLength(1).WithMessage("Body must not be less than 1 characters");

        RuleFor(e => e.Attachments)
            .ForEach(a =>
            {
                a.Must(attachments => Validators.IsSizeAllowed(attachments, 25000));
            }).WithMessage("The size of the attachments must be less than 25 MB.");
    }
}