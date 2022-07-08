using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Service.Dtos.Account
{
    public class RegisterDto
    {
        public string? Fullname { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? ProfilePhoto { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }

    public class RegisetDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisetDtoValidator()
        {
            RuleFor(x => x.Fullname).NotEmpty().NotNull().MinimumLength(3).MaximumLength(250);
            RuleFor(x => x.Username).NotEmpty().NotNull().MinimumLength(3).MaximumLength(250);
            RuleFor(x => x.Email).NotEmpty().NotNull().MinimumLength(5).MaximumLength(80).EmailAddress();
            RuleFor(x => x.Password).NotEmpty().NotNull().MinimumLength(8).MaximumLength(100);
            RuleFor(x => x.ConfirmPassword).NotEmpty().NotNull().MinimumLength(8).MaximumLength(100).Equal(x => x.Password);
            RuleFor(x => x.ProfilePhoto).MinimumLength(3).MaximumLength(300);
        }
    }
}
