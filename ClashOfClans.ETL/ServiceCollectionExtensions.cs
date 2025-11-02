using ClashOfClans.ETL.Jobs;
using ClashOfClans.ETL.Services;
using ClashOfClans.ETL.Services.Integration;
using Microsoft.Extensions.DependencyInjection;

namespace ClashOfClans.ETL;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddClashOfClansServices(this IServiceCollection services)
    {
        services.AddSingleton<ClashOfClansService>();
        services.AddTransient<BuscarClanJob>();
        services.AddTransient<EnviarGuerraJob>();
        services.AddTransient<IntegrationService>();

        return services;
    }
}