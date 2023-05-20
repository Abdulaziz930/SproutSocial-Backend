using SproutSocial.Domain.Enums;

namespace SproutSocial.Domain.Entities.Identity;

public class TwoFaMethod : BaseEntity
{
    public TwoFactorAuthMethod TwoFactorAuthMethod { get; set; }
    public ICollection<UserTwoFaMethod> UserTwoFaMethods { get; set; }
}