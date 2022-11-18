namespace SproutSocial.Application.DTOs.BlogDtos;

public record BlogLikeDto
{
    public Guid UserId { get; init; }
    public string UserName { get; init; } = null!;
}