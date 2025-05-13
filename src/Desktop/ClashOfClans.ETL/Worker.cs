using Microsoft.Extensions.Hosting;

namespace ClashOfClans.ETL;

public class Worker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public Worker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        BackgroundServer backgroundServer = new(_serviceProvider);
    }
}

public enum IntervalSimpleSchedule
{
    Hours,
    Minutes,
    Seconds
}