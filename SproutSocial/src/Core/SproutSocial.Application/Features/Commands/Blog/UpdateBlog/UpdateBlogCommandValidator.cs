namespace SproutSocial.Application.Features.Commands.Blog.UpdateBlog;

public class UpdateBlogCommandValidator : AbstractValidator<UpdateBlogCommandRequest>
{
    public UpdateBlogCommandValidator()
    {
        RuleFor(b => b.Id)
            .NotNull().WithMessage("Blog id is required")
            .NotEmpty().WithMessage("Blog id cannot be empty")
            .Custom((id, context) =>
            {
                if (!Validators.IsGuid(id))
                    context.AddFailure("Please enter correct blog id");
            });

        RuleFor(b => b.Title)
           .NotNull().WithMessage("Blog name is required")
           .NotEmpty().WithMessage("Blog name cannot be empty")
           .MinimumLength(3).WithMessage("Blgo name must not be less than 3 characters")
           .MaximumLength(100).WithMessage("Blog name must not exceed 100 characters");

        RuleFor(b => b.Content)
            .NotNull().WithMessage("Blog content is required")
            .NotEmpty().WithMessage("Blog content cannot be empty")
            .MaximumLength(1500).WithMessage("Blog content must not exceed 1500 characters");

        RuleFor(b => b.FormFile)
            .Custom((image, context) =>
            {
                if (image != null)
                {
                    if (!Validators.IsImage(image))
                        context.AddFailure("File mimetype must be image/*");

                    if (!Validators.IsSizeAllowed(image, 3000))
                        context.AddFailure("The size of the image must be less than 3 MB.");
                }
            });

        RuleFor(b => b.TopicIds)
            .Custom((topicIds, context) =>
            {
                if (topicIds != null && topicIds.Count != 0)
                {
                    foreach (var topicId in topicIds)
                    {
                        if (string.IsNullOrWhiteSpace(topicId))
                            context.AddFailure("TopicId cannot empty");

                        if (!Validators.IsGuid(topicId))
                            context.AddFailure("Please enter correct topicId");
                    }
                }
            });
    }
}
