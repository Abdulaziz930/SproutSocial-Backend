using Microsoft.AspNetCore.Identity;

namespace SproutSocial.API.Extensions.ServiceExtensions
{
    public static class ConfigurationServiceExtension
    {
        public static void AddConfigureService(this IServiceCollection services)
        {
            services.Configure<DataProtectionTokenProviderOptions>(options =>
                options.TokenLifespan = TimeSpan.FromMinutes(15));
        }
    }
}
