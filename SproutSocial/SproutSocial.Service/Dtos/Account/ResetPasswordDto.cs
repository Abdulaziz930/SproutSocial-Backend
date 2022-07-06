using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Service.Dtos.Account
{
    public class ResetPasswordDto
    {
        public string? Token { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }

    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator()
        {
            RuleFor(x => x.Token).NotEmpty().NotNull();
            RuleFor(x => x.Password).NotEmpty().NotNull().MinimumLength(8).MaximumLength(100);
            RuleFor(x => x.ConfirmPassword).NotEmpty().NotNull().MinimumLength(8).MaximumLength(100).Equal(x => x.Password);
        }
    }
}
