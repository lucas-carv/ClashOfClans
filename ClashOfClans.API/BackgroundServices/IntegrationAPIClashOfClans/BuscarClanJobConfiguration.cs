using Quartz;

namespace ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans;

public static class BuscarClanJobConfiguration
{
    public static void AddBuscarClanJob(this IServiceCollectionQuartzConfigurator configurator)
    {
        JobKey jobKey = new(nameof(BuscarClanJob));
        configurator.AddJob<BuscarClanJob>(opts => opts.WithIdentity(jobKey));

        configurator.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity($"{nameof(BuscarClanJob)}-trigger")
            .WithSimpleSchedule(x => x.WithIntervalInMinutes(5)
            .RepeatForever())
            );
    }
}
