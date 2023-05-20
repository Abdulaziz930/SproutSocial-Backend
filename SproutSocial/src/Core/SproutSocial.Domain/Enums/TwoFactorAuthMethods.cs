using System.ComponentModel.DataAnnotations;

namespace SproutSocial.Domain.Enums;

public enum TwoFactorAuthMethod : byte
{
    None,
    Email,
    [Display(Name = "Google Authenticator")]
    GoogleAuthenticator
}