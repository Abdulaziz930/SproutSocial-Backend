namespace SproutSocial.Application.DTOs.UserDtos;

public class UserInfoDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = null!;
}

public class UserAuditableDto : UserInfoDto
{
    public string? Fullname { get; set; }
    public string Email { get; set; } = null!;
}
