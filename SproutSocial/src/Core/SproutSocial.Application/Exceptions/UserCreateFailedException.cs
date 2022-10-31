namespace SproutSocial.Application.Exceptions;

public sealed class UserCreateFailedException : Exception
{
    public UserCreateFailedException() : base("Unexpected error occurred while creating user")
    {
    }

    public UserCreateFailedException(string? message) : base(message)
    {
    }

    public UserCreateFailedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
