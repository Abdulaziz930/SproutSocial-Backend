namespace SproutSocial.Application.Exceptions;

public sealed class RecordAlreadyExistException : Exception
{
    public RecordAlreadyExistException() : base("Entity already exist")
    {
    }

    public RecordAlreadyExistException(string? message) : base(message)
    {
    }

    public RecordAlreadyExistException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
