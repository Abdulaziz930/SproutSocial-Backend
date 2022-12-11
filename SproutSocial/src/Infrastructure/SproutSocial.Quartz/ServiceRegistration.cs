using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using SproutSocial.Quartz.Jobs;
using SproutSocial.Quartz.Models;
using SproutSocial.Quartz.Services;

namespace SproutSocial.Quartz;

public static class ServiceRegistration
{
    public static IServiceCollection AddQuartzServices(this IServiceCollection services)
    {
        services.AddSingleton<IJobFactory, SingletonJobFactory>();
        services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

        services.AddSingleton<BirthDayJob>();

        services.AddSingleton(new JobSchedule(
            jobType: typeof(BirthDayJob), 
            cronExpression: Configuration.GetCronExpression(nameof(BirthDayJob))
            ));

        services.AddHostedService<QuartzHostedService>();

        return services;
    }
}