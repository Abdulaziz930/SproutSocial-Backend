namespace SproutSocial.Application.Abstractions.Common;

public interface IDateTime
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
}
