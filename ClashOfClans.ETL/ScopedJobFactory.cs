using Quartz;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Spi;

namespace ClashOfClans.ETL;

public class ScopedJobFactory : IJobFactory
{
    private readonly IServiceProvider _provider;

    public ScopedJobFactory(IServiceProvider provider)
    {
        _provider = provider;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        return (IJob)_provider.GetRequiredService(bundle.JobDetail.JobType);
    }

    public void ReturnJob(IJob job)
    {
        // Nada aqui
    }
}