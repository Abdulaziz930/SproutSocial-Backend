namespace SproutSocial.API.Extensions.ServiceExtensions;

public static class ApiVersionServiceExtension
{
    public static IServiceCollection AddApiVersionService(this IServiceCollection services, string version)
    {
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.DefaultApiVersion = new ApiVersion(Convert.ToInt32(version), 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
        });
        return services;
    }
}
