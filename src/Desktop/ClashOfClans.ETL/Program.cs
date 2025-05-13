using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ClashOfClans.ETL;
using ClashOfClans.ETL.ClashOfClans.Services;
using ClashOfClans.ETL.ClashOfClans;

Host.CreateDefaultBuilder(args)
    .UseWindowsService() 
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<IClanApiService, ClanApiService>();
        services.AddSingleton<BuscarGuerrasJob>();
    })
    .Build()
    .Run();