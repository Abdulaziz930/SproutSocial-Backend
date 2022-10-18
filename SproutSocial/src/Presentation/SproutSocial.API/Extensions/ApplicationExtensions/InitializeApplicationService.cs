using SproutSocial.Persistence.Contexts;

namespace SproutSocial.API.Extensions.ApplicationExtensions;

public static class InitializeApplicationService
{
    public static IApplicationBuilder AddInitializeApplicationService(this IApplicationBuilder builder)
    {
        using (var scope = builder.ApplicationServices.CreateScope())
        {
            var initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
            initializer.InitializeAsync().Wait();
            initializer.SeedAsync().Wait();
        }

        return builder;
    }
}
