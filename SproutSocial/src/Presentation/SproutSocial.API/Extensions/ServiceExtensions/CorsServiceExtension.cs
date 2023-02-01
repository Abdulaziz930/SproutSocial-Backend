namespace SproutSocial.API.Extensions.ServiceExtensions;

public static class CorsServiceExtension
{
    public static IServiceCollection AddCorsService(this IServiceCollection services, string[] origins)
    {
        services.AddCors(options => options.AddDefaultPolicy(policy =>
           policy.WithOrigins(origins)
           .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials()
        ));
        return services;
    }
}