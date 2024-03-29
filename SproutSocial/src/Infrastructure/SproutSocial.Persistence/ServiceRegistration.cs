﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SproutSocial.Application.Abstractions.Services;
using SproutSocial.Application.Helpers.Extesions;
using SproutSocial.Domain.Entities.Identity;
using SproutSocial.Persistence.MappingProfiles;
using SproutSocial.Persistence.Services;
using SproutSocial.Persistence.TokenProviders;

namespace SproutSocial.Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(Configuration.ConnectionString);
        });

        services.AddScoped<GoogleAuthenticatorTokenProvider<AppUser>>();

        services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
        {
            options.User.RequireUniqueEmail = true;

            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;

            options.SignIn.RequireConfirmedEmail = true;

            options.Tokens.ProviderMap["Google Authenticator"] = new TokenProviderDescriptor(
                typeof(GoogleAuthenticatorTokenProvider<AppUser>));
        }).AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

        services.Configure<DataProtectionTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromHours(1);
        });

        services.AddScoped<AppDbContextInitializer>();

        services.AddAutoMapperProfiles<TopicMapper>();

        services.AddScoped<ITopicReadRepository, TopicReadRepository>();

        services.RegisterServices<ITopicService, TopicService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        return services;
    }
}
