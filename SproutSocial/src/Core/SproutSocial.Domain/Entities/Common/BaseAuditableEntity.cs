namespace SproutSocial.Domain.Entities.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTime CreatedDate { get; set; }
    public virtual string? CreatedBy { get; set; }
    public virtual DateTime LastModifiedDate { get; set; }
    public virtual string? LastModifiedBy { get; set; }
    public virtual bool IsDeleted { get; set; }
}
