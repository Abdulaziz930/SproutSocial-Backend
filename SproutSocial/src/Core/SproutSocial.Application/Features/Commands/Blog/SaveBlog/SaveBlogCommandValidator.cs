namespace SproutSocial.Application.Features.Commands.Blog.SaveBlog;

public class SaveBlogCommandValidator : AbstractValidator<SaveBlogCommandRequest>
{
    public SaveBlogCommandValidator()
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