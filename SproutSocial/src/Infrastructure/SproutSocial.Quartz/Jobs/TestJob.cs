using Quartz;

namespace SproutSocial.Quartz.Jobs;

[DisallowConcurrentExecution]
public class TestJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("test job executed");
        return Task.CompletedTask;
    }
}
