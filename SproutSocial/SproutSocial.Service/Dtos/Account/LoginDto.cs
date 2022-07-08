using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Service.Dtos.Account
{
    public class LoginDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty().NotNull().MinimumLength(3).MaximumLength(250);
            RuleFor(x => x.Password).NotEmpty().NotNull().MinimumLength(8).MaximumLength(100);
        }
    }
}
