using Quartz.Impl.Matchers;
using Quartz.Impl;
using Quartz;

namespace ClashOfClans.ETL;

public class BackgroundServer : IDisposable
{
    IScheduler Scheduler;
    public BackgroundServer(IServiceProvider provider)
    {
        Scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;

        Scheduler.JobFactory = new ScopedJobFactory(provider);
        Scheduler.Start();
        Console.WriteLine("Iniciado");
        Scheduler.ListenerManager.AddJobListener(new GlobalJobListener(), GroupMatcher<JobKey>.AnyGroup());


        this.LoadJobs();

    }
    public void AddJob<T>(IntervalSimpleSchedule interval, int value, string group = "default") where T : IJob
    {
        string jobName = typeof(T).Name;

        ITrigger trigger = TriggerBuilder.Create()
         .WithIdentity($"trigger-{jobName}", group)
         .StartNow()
         .WithSimpleSchedule(x =>
         {
             switch (interval)
             {
                 case IntervalSimpleSchedule.Hours:
                     x.WithIntervalInHours(value).RepeatForever();
                     break;
                 case IntervalSimpleSchedule.Minutes:
                     x.WithIntervalInMinutes(value).RepeatForever();
                     break;
                 default:
                     x.WithIntervalInSeconds(value).RepeatForever();
                     break;
             }
         }).Build();

        AddJob<T>(trigger);
    }

    public void AddJob<T>(ITrigger trigger) where T : IJob
    {
        Console.WriteLine($"Agendando job: {typeof(T).Name}");
        string jobName = typeof(T).Name;

        IJobDetail job = JobBuilder.Create<T>()
                .WithIdentity(jobName, trigger.Key.Group)
                .Build();

        Scheduler.ScheduleJob(job, trigger);
    }

    public void Dispose()
    {
    }
}
public enum IntervalSimpleSchedule
{
    Hours,
    Minutes,
    Seconds
}
