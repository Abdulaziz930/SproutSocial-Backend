using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SproutSocial.Core.Entities;
using SproutSocial.Data;
using SproutSocial.Data.Identity;

namespace SproutSocial.API.Extensions.ApplicationExtensions
{
    public static class SeedDataExtension
    {
        public static void UseSeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>())
                {
                    appDbContext.Database.Migrate();
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    DataInitializer.SeedData(roleManager, userManager);
                }
            }
        }
    }
}
