namespace SproutSocial.Application.Features.Commands.Blog.RemoveSavedBlog;

public class RemoveSavedBlogCommandValidator : AbstractValidator<RemoveSavedBlogCommandRequest>
{
    public RemoveSavedBlogCommandValidator()
    {
        RuleFor(b => b.BlogId)
            .NotNull().WithMessage("Blog id is required")
            .NotEmpty().WithMessage("Blog id cannot be empty")
            .Custom((id, context) =>
            {
                if (!Validators.IsGuid(id))
                    context.AddFailure("Please enter correct blog id");
            });
    }
}
