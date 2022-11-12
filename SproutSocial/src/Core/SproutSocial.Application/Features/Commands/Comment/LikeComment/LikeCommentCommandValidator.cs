namespace SproutSocial.Application.Features.Commands.Comment.LikeComment;

public class LikeCommentCommandValidator : AbstractValidator<LikeCommentCommandRequest>
{
    public LikeCommentCommandValidator()
    {
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
