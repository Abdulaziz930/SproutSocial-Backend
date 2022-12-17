using System.ComponentModel.DataAnnotations.Schema;

namespace SproutSocial.Domain.Entities;

public class Subscribe : BaseAuditableEntity
{
    public string Email { get; set; } = null!;

    [NotMapped]
    public override bool IsDeleted { get; set; }
    [NotMapped]
    public override string? CreatedBy { get; set; }
    [NotMapped]
    public override DateTime LastModifiedDate { get; set; }
    [NotMapped]
    public override string? LastModifiedBy { get; set; }
}