namespace SproutSocial.Application.DTOs.UserDtos;

public record SelectTwoFaMethodResponseDto
{
    public HttpStatusCode StatusCode { get; init; }
    public string? Message { get; init; }
}