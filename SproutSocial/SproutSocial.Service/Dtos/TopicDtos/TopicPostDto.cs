using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Service.Dtos.TopicDtos
{
    public class TopicPostDto
    {
        public string? Name { get; set; }
    }

    public class TopicPostDtoValidator : AbstractValidator<TopicPostDto>
    {
        public TopicPostDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MinimumLength(2).MaximumLength(100);    
        }
    }
}
