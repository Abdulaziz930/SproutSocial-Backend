using Microsoft.AspNetCore.Identity;

namespace SproutSocial.Domain.Entities.Identity;

public class AppUser : IdentityUser
{
    public string? Fullname { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenEndDate { get; set; }
}
