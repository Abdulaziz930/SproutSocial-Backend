using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Service.Dtos.Account
{
    public class ForgotPasswordDto
    {
        public string? Email { get; set; }
    }

    public class ForgotPasswordDtoValidator : AbstractValidator<ForgotPasswordDto>
    {
        public ForgotPasswordDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().MinimumLength(5).MaximumLength(80).EmailAddress();   
        }
    }
}
