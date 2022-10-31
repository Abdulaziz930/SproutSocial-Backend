namespace SproutSocial.Application.Exceptions;

public sealed class PageFormatException : Exception
{
    public PageFormatException() : base("Page must be greater or equal than 1")
    {
    }

    public PageFormatException(string? message) : base(message)
    {
    }

    public PageFormatException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
