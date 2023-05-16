using Microsoft.AspNetCore.Identity;

namespace SproutSocial.Application.Exceptions.Authentication;

public sealed class EmailConfirmationException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }
    public string? ErrorDetail { get; }

    public EmailConfirmationException()
    {
        ErrorMessage = "Unexpected error occurred while activate user";
    }

    public EmailConfirmationException(IEnumerable<IdentityError> errors)
    {
        ErrorMessage = "Unexpected error occurred while activate user";
        ErrorDetail = String.Join(',', errors.Select(e => e.Description));
    }

    public EmailConfirmationException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public EmailConfirmationException(string message, Exception? innerException) : base(message, innerException)
    {
        ErrorMessage = message;
    }
}
