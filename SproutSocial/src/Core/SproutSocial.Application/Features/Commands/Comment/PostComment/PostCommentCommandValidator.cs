namespace SproutSocial.Application.Features.Commands.Comment.PostComment;

public class PostCommentCommandValidator : AbstractValidator<PostCommentCommandRequest>
{
    public PostCommentCommandValidator()
    {
        RuleFor(c => c.Message)
            .NotNull().WithMessage("Comment message cannot be null")
            .NotEmpty().WithMessage("Comment message cannot be empty")
            .MinimumLength(2).WithMessage("Comment message must not be less than 2 characters")
            .MaximumLength(300).WithMessage("Comment message must not exceed 300 characters");

        RuleFor(c => c.BlogId)
            .NotNull().WithMessage("Blog id is required")
            .NotEmpty().WithMessage("Blog id cannot be empty")
            .Custom((id, context) =>
            {
                if (!Validators.IsGuid(id))
                    context.AddFailure("Please enter correct blog id");
            });

        RuleFor(c => c.CommentId)
            .Custom((id, context) =>
            {
                if (!string.IsNullOrWhiteSpace(id))
                {
                    if (!Validators.IsGuid(id))
                        context.AddFailure("Please enter correct comment id");
                }
            });
    }
}
