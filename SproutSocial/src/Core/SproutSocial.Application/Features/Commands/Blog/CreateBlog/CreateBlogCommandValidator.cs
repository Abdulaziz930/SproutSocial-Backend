using SproutSocial.Application.Helpers;

namespace SproutSocial.Application.Features.Commands.Blog.CreateBlog;

public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommandRequest>
{
    public CreateBlogCommandValidator()
    {
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
            .NotNull()
            .WithMessage("Blog image is required")
            .Must(Validators.IsImage)
            .WithMessage("File mimetype must be image/*")
            .Must(formFile => Validators.IsSizeAllowed(formFile, 3000))
            .WithMessage("The size of the image must be less than 3 MB.");

        RuleFor(b => b.TopicIds)
            .NotNull()
            .Custom((topicIds, context) =>
            {
                if (topicIds.Count == 0)
                    context.AddFailure("TopicIds cannot empty");

                foreach (var topicId in topicIds)
                {
                    if (string.IsNullOrWhiteSpace(topicId))
                        context.AddFailure("TopicId cannot empty");

                    if (!Validators.IsGuid(topicId))
                        context.AddFailure("Please enter correct topicId");
                }
            });
    }
}
