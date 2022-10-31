namespace SproutSocial.Application.Exceptions;

public sealed class NotFoundException : Exception
{
    public NotFoundException() : base("Not found")
    {
    }

    public NotFoundException(string? message) : base(message)
    {
    }

    public NotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public NotFoundException(string name, string key) : base($"Entity \"{name}\" {key} was not found")
    {
    }
}
