using Quartz.Impl;
using Quartz;
using ClashOfClans.ETL.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace ClashOfClans.ETL;

internal class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection().AddClashOfClansServices(); 

        var provider = services.BuildServiceProvider();

        using var server = new BackgroundServer(provider);
        server.AddClashOfClansJobs();

        await Task.Delay(-1);
    }
}
