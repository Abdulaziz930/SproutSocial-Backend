using Microsoft.EntityFrameworkCore;
using SproutSocial.Data;

namespace SproutSocial.API.Extensions.ServiceExtensions
{
    public static class DatabaseConnectionExtension
    {
        public static void AddDatabaseConnectionService(this IServiceCollection services, IConfiguration configuration, string section)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(section));
            });
        }
    }
}
