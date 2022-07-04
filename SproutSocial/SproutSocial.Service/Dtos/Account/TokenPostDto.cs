using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Service.Dtos.Account
{
    public class TokenPostDto
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }

    public class TokenPostValidator : AbstractValidator<TokenPostDto>
    {
        public TokenPostValidator()
        {
            RuleFor(x => x.AccessToken).NotEmpty().NotNull();
            RuleFor(x => x.RefreshToken).NotEmpty().NotNull();
        }
    }
}
