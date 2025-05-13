using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ClashOfClans.ETL;
using ClashOfClans.ETL.ClashOfClans.Services;
using ClashOfClans.ETL.ClashOfClans;
using ClashOfClans.ETL.Data;
using System.Net.Http;

Host.CreateDefaultBuilder(args)
    .UseWindowsService() 
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<IClanApiService, ClanApiService>();
        services.AddSingleton<BuscarGuerrasJob>();
        services.AddScoped<ClashOfClansRepository>();
        services.AddSingleton<SincronizadorConfiguration>();
        services.AddHttpClient();
    })
    .Build()
    .Run();
