namespace SproutSocial.Application.Exceptions;

public sealed class AuthenticationFailException : Exception
{
    public AuthenticationFailException() : base("Username or password incorrect")
    {
    }

    public AuthenticationFailException(string? message) : base(message)
    {
    }

    public AuthenticationFailException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
