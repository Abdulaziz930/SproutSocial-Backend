namespace SproutSocial.Domain.Entities.Identity;

public class UserTwoFaMethod : BaseEntity
{
    public Guid UserId { get; set; }
    public AppUser AppUser { get; set; }
    public Guid TwoFaMethodId { get; set; }
    public TwoFaMethod TwoFaMethod { get; set; }
    public bool IsSelected { get; set; }
}