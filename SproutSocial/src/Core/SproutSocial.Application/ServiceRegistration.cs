using Microsoft.Extensions.DependencyInjection;

namespace SproutSocial.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(ServiceRegistration));
        return services;
    }
}