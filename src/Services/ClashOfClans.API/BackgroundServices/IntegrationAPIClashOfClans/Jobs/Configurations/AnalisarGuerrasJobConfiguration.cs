using Quartz;

namespace ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Jobs.Configurations;

public static class AnalisarGuerrasJobConfiguration
{
    public static void AddAnalisarGuerras(this IServiceCollectionQuartzConfigurator configurator)
    {
        JobKey jobKey = new(nameof(AnalisarGuerrasJob));
        configurator.AddJob<AnalisarGuerrasJob>(opts => opts.WithIdentity(jobKey));

        configurator.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity($"{nameof(AnalisarGuerrasJob)}-trigger")
            .StartNow()
            //.StartAt(DateBuilder.FutureDate(1, IntervalUnit.Minute))
            .WithSimpleSchedule(x => x.WithIntervalInMinutes(5)
            .RepeatForever())
            );
    }
}
