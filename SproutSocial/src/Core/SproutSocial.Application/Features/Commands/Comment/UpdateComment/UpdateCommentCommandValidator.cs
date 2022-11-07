namespace SproutSocial.Application.Features.Commands.Comment.UpdateComment;

public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommandRequest>
{
    public UpdateCommentCommandValidator()
    {
        RuleFor(c => c.Message)
            .NotNull().WithMessage("Comment message cannot be null")
            .NotEmpty().WithMessage("Comment message cannot be empty")
            .MinimumLength(2).WithMessage("Comment message must not be less than 2 characters")
            .MaximumLength(300).WithMessage("Comment message must not exceed 300 characters");

        RuleFor(c => c.CommentId)
            .NotNull().WithMessage("Comment id is required")
            .NotEmpty().WithMessage("Comment id cannot be empty")
            .Custom((id, context) =>
            {
                if (!Validators.IsGuid(id))
                    context.AddFailure("Please enter correct comment id");
            });
    }
}