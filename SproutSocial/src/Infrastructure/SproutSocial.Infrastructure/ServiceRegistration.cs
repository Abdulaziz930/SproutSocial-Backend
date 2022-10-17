using Microsoft.Extensions.DependencyInjection;
using SproutSocial.Infrastructure.Services.Common;

namespace SproutSocial.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrasturctureServices(this IServiceCollection services)
    {
        services.AddTransient<IDateTime, DateTimeService>();
        return services;
    }
}
