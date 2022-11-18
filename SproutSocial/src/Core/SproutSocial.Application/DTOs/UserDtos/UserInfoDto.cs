namespace SproutSocial.Application.DTOs.UserDtos;

public record UserInfoDto
{
    public Guid Id { get; init; }
    public string UserName { get; init; } = null!;
}

public record UserAuditableDto : UserInfoDto
{
    public string? Fullname { get; init; }
    public string Email { get; init; } = null!;
}