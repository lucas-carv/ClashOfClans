using Quartz;

namespace ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Jobs.Configurations;

public static class DetectarMembrosInativosEmGuerrasJobConfiguration
{
    public static void AddDetectarMembrosInativosJob(this IServiceCollectionQuartzConfigurator configurator)
    {
        JobKey jobKey = new(nameof(DetectarMembrosInativosEmGuerrasJob));
        configurator.AddJob<DetectarMembrosInativosEmGuerrasJob>(opts => opts.WithIdentity(jobKey));

        configurator.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity($"{nameof(DetectarMembrosInativosEmGuerrasJob)}-trigger")
            .StartNow()
            .WithSimpleSchedule(x => x.WithIntervalInHours(2)
            .RepeatForever())
            );
    }
}
