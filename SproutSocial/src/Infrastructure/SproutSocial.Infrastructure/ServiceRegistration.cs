using Microsoft.Extensions.DependencyInjection;
using SproutSocial.Application.Abstractions.Email;
using SproutSocial.Application.Abstractions.Storage;
using SproutSocial.Application.Abstractions.Token;
using SproutSocial.Infrastructure.Services.Common;
using SproutSocial.Infrastructure.Services.Email;
using SproutSocial.Infrastructure.Services.Storage;
using SproutSocial.Infrastructure.Services.Token;

namespace SproutSocial.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrasturctureServices(this IServiceCollection services)
    {
        services.AddScoped<IStorageService, StorageService>();
        services.AddScoped<ITokenHandler, TokenHandler>();

        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IMailService, MailService>();

        services.AddSingleton<IBaseUrlAccessor, BaseUrlAccessor>();
        return services;
    }

    public static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage
    {
        services.AddScoped<IStorage, T>();
    }
}
