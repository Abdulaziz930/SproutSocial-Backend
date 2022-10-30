namespace SproutSocial.Application.Features.Queries.Blog.GetBlogById;

public class GetBlogByIdQueryValidator : AbstractValidator<GetBlogByIdQueryRequest>
{
    public GetBlogByIdQueryValidator()
    {
        RuleFor(b => b.Id)
            .NotNull().WithMessage("Blog id is required")
            .NotEmpty().WithMessage("Blog id cannot be empty")
            .Must(Validators.IsGuid).WithMessage("Please enter your id correctly");
    }
}
