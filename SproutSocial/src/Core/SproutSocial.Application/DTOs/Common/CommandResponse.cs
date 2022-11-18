namespace SproutSocial.Application.DTOs.Common;

public record CommandResponse
{
    public HttpStatusCode StatusCode { get; init; }
    public string? Message { get; init; }
}