using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SproutSocial.Application.Abstractions.Services;
using SproutSocial.Persistence.MappingProfiles;
using SproutSocial.Persistence.Services;

namespace SproutSocial.Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = Configuration.ConnectionString;
            options.UseSqlServer(Configuration.ConnectionString);
        });

        services.AddSingleton(provider => new MapperConfiguration(mc =>
        {
            mc.AddProfile(new AutoMapperProfile());
        }).CreateMapper());

        services.AddScoped<ITopicReadRepository, TopicReadRepository>();

        services.AddScoped<ITopicService, TopicService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        return services;
    }
}
