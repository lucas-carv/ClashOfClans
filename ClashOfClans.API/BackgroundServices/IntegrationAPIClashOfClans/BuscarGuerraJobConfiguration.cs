using Quartz;

namespace ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans;

public static class BuscarGuerraJobConfiguration
{
    public static void AddBuscarGuerraJob(this IServiceCollectionQuartzConfigurator configurator)
    {
        JobKey jobKey = new(nameof(BuscarGuerraJob));
        configurator.AddJob<BuscarGuerraJob>(opts => opts.WithIdentity(jobKey));

        configurator.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity($"{nameof(BuscarGuerraJob)}-trigger")
            .WithSimpleSchedule(x => x.WithIntervalInMinutes(5)
            .RepeatForever())
            );
    }
}
