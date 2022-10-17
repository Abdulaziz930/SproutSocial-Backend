using Microsoft.EntityFrameworkCore;
using SproutSocial.Persistence.Contexts;

namespace SproutSocial.API.Extensions;

public static class MigrationServiceExtension
{
    public static void AddMigrationService(this IApplicationBuilder builder)
    {
        using (var scope = builder.ApplicationServices.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
        }
    }
}
