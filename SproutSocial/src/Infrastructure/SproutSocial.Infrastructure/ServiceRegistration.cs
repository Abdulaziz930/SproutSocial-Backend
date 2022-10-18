using Microsoft.Extensions.DependencyInjection;
using SproutSocial.Application.Abstractions.Token;
using SproutSocial.Infrastructure.Services.Common;
using SproutSocial.Infrastructure.Services.Token;

namespace SproutSocial.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrasturctureServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenHandler, TokenHandler>();

        services.AddTransient<IDateTime, DateTimeService>();
        return services;
    }
}
