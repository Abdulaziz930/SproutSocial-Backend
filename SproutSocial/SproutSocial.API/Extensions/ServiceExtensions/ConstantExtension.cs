
using SproutSocial.Service.Utils;

namespace SproutSocial.API.Extensions.ServiceExtensions
{
    public static class ConstantExtension
    {
        public static void AddConstants(this IServiceCollection services, IConfiguration configuration)
        {
            Constants.EmailAddress = configuration["Gmail:Address"];
            Constants.EmailPassword = configuration["Gmail:Password"];
        }
    }
}
