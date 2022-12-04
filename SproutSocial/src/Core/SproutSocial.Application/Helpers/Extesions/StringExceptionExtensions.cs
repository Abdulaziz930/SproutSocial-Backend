namespace SproutSocial.Application.Helpers.Extesions;

public static class StringExceptionExtensions
{
    /// <summary>
    /// ArgumentException throw if value is null or whitespace
    /// </summary>
    /// <param name="value">Value to check</param>
    /// <param name="message">Error message</param>
    /// <exception cref="ArgumentException"></exception>
    public static void ThrowIfNullOrWhiteSpace(this string value, string? message = null)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException(message ?? "Value cannot be null or whitespace");
    }

    /// <summary>
    /// ArgumentException throw if value is null or empty
    /// </summary>
    /// <param name="value">Value to check</param>
    /// <param name="message">Error message</param>
    /// <exception cref="ArgumentException"></exception>
    public static void ThrowIfNullOrEmpty(this string value, string? message = null)
    {
        if (string.IsNullOrEmpty(value))
            throw new ArgumentException(message ?? "Value cannot be null or empty");
    }
}
