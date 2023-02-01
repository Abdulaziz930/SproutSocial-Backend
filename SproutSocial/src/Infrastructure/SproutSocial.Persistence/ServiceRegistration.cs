using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SproutSocial.Application.Abstractions.Services;
using SproutSocial.Application.Helpers.Extesions;
using SproutSocial.Domain.Entities.Identity;
using SproutSocial.Persistence.MappingProfiles;
using SproutSocial.Persistence.Services;

namespace SproutSocial.Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(Configuration.ConnectionString);
        });
        services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
        {
            options.User.RequireUniqueEmail = true;

            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;
        }).AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

        services.AddScoped<AppDbContextInitializer>();

        services.AddAutoMapperProfiles<TopicMapper>();

        services.AddScoped<ITopicReadRepository, TopicReadRepository>();

        services.RegisterServices<ITopicService, TopicService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        return services;
    }
}
