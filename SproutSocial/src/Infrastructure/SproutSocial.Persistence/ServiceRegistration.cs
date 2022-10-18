using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SproutSocial.Application.Abstractions.Services;
using SproutSocial.Domain.Entities.Identity;
using SproutSocial.Persistence.Contexts;
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
        services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;

            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;
        }).AddEntityFrameworkStores<AppDbContext>();

        services.AddScoped<AppDbContextInitializer>();

        services.AddSingleton(provider => new MapperConfiguration(mc =>
        {
            mc.AddProfile(new AutoMapperProfile());
        }).CreateMapper());

        services.AddScoped<ITopicReadRepository, TopicReadRepository>();

        services.AddScoped<ITopicService, TopicService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        return services;
    }
}
