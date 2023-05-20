using SproutSocial.Domain.Enums;

namespace SproutSocial.Application.Exceptions.Authentication
{
    public sealed class TwoFaTypeAlreadyRegisteredException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage { get; }

        public TwoFaTypeAlreadyRegisteredException(TwoFactorAuthMethod twoFactorAuthMethod)
        {
            ErrorMessage = $"{EnumHelper.GetEnumDisplayName(twoFactorAuthMethod)} 2FA type already registered";
        }

        public TwoFaTypeAlreadyRegisteredException(string message) : base(message)
        {
            ErrorMessage = message;
        }

        public TwoFaTypeAlreadyRegisteredException(string message, Exception? innerException) : base(message, innerException)
        {
            ErrorMessage = message;
        }
    }
}
