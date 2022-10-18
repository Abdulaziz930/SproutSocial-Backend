﻿namespace SproutSocial.Application.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base("Username or password invalid")
    {
    }

    public UserNotFoundException(string? message) : base(message)
    {
    }

    public UserNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
