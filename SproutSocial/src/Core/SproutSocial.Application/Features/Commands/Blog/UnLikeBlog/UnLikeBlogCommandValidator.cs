namespace SproutSocial.Application.Features.Commands.Blog.UnLikeBlog;

public class UnLikeBlogCommandValidator : AbstractValidator<UnLikeBlogCommandRequest>
{
    public UnLikeBlogCommandValidator()
    {
        RuleFor(b => b.Id)
            .NotNull().WithMessage("Blog id is required")
            .NotEmpty().WithMessage("Blog id cannot be empty")
            .Custom((id, context) =>
            {
                if (!Validators.IsGuid(id))
                    context.AddFailure("Please enter correct blog id");
            });
    }
}