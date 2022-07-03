using SproutSocial.Service.HelperServices.Implementations;
using SproutSocial.Service.HelperServices.Interfaces;

namespace SproutSocial.API.Extensions.ServiceExtensions
{
    public static class RegisterHelperServiceExtension
    {
        public static void AddHelperService(this IServiceCollection services)
        {
            services.AddSingleton<IHelperAccessor, HelperAccessor>();
            services.AddScoped<IFileManager, FileManager>();
        }
    }
}
