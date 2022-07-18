using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Service.Dtos.BlogDtos
{
    public class BlogPostDto
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public IFormFile? File { get; set; }
        public string? UserId { get; set; }
        public ICollection<BlogTopicDto>? BlogTopics { get; set; }
    }

    public class BlogTopicDtoValidator : AbstractValidator<BlogPostDto>
    {
        public BlogTopicDtoValidator()
        {
            RuleFor(b => b.Title).NotEmpty().NotNull().MinimumLength(3).MaximumLength(250);
            RuleFor(b => b.Content).NotEmpty().NotNull().MinimumLength(3).MaximumLength(500);
            RuleFor(b => b.UserId).NotEmpty().NotNull();
            RuleForEach(b => b.BlogTopics).NotNull().Custom((x, context) =>
            {
                if (x.Id is null)
                    context.AddFailure(new FluentValidation.Results.ValidationFailure 
                    { 
                        PropertyName = "TopicId", ErrorMessage = "Id cannot be null"
                    });
            });
        }
    }
}
