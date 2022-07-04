using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Service.Dtos.Account
{
    public class ChangePasswordDto
    {
        public string? Username { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
    }

    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty().NotNull().MinimumLength(3).MaximumLength(250);
            RuleFor(x => x.OldPassword).NotEmpty().NotNull().MinimumLength(8).MaximumLength(100);
            RuleFor(x => x.NewPassword).NotEmpty().NotNull().MinimumLength(8).MaximumLength(100);
            RuleFor(x => x.ConfirmPassword).Null().NotEmpty().MinimumLength(8).MaximumLength(100).Equal(x => x.NewPassword);
        }
    }
}
