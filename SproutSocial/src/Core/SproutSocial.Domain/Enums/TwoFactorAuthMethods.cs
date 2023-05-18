using System.ComponentModel.DataAnnotations;

namespace SproutSocial.Domain.Enums;

public enum TwoFactorAuthMethod
{
    Email = 1,
    [Display(Name = "Google Authenticator")]
    GoogleAuthenticator
}