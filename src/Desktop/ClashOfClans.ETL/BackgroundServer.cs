using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Quartz.Spi;
using System;

namespace ClashOfClans.ETL;

public class BackgroundServer : IDisposable
{
    IScheduler Scheduler;

    public BackgroundServer(IServiceProvider serviceProvider)
    {
        var schedulerFactory = new StdSchedulerFactory();
        Scheduler = schedulerFactory.GetScheduler().Result;

        // Aqui é onde a mágica acontece:
        Scheduler.JobFactory = new ScopedJobFactory(serviceProvider);

        Scheduler.ListenerManager.AddJobListener(new GlobalJobListener(), GroupMatcher<JobKey>.AnyGroup());
        Scheduler.Start();

        this.LoadJobs();
    }

    public void AddJob<T>(ITrigger trigger) where T : IJob
    {
        string jobName = typeof(T).Name;

        IJobDetail job = JobBuilder.Create<T>()
                .WithIdentity(jobName, trigger.Key.Group)
                .Build();

        Scheduler.ScheduleJob(job, trigger);
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
    public void Dispose()
    {

    }
}
public class ScopedJobFactory : IJobFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ScopedJobFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        return (IJob)_serviceProvider.GetRequiredService(bundle.JobDetail.JobType);
    }

    public void ReturnJob(IJob job)
    {
        // Nada a fazer aqui
    }
}