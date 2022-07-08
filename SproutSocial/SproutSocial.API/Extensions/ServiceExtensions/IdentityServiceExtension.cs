using Microsoft.AspNetCore.Identity;
using SproutSocial.Data;

namespace SproutSocial.API.Extensions.ServiceExtensions
{
    public static class IdentityServiceExtension
    {
        public static void AddIdentityService<U,R>(this IServiceCollection services) where U : IdentityUser, new()
                                                                                     where R : IdentityRole, new()
        {
            services.AddIdentity<U, R>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;

                options.User.RequireUniqueEmail = true;

                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        }
    }
}
