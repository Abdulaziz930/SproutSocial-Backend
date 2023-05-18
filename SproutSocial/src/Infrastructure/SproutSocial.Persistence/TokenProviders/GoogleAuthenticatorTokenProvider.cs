using Microsoft.AspNetCore.Identity;

namespace SproutSocial.Persistence.TokenProviders;

public class GoogleAuthenticatorTokenProvider<TUser> : TotpSecurityStampBasedTokenProvider<TUser>
where TUser : class
{
    // No need to implement anything, use the base class
    public override Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user)
    {
        return Task.FromResult(true);
    }
}
