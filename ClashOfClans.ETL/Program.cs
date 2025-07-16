using Quartz.Impl;
using Quartz;
using ClashOfClans.ETL.Jobs;
using ClashOfClans.ETL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ClashOfClans.ETL;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Iniciando servidor de jobs...");

        // Criação do container e registro via extensão
        var services = new ServiceCollection()
            .AddClashOfClansServices(); // <-- Sua extensão

        // Corrigir erro: garantir que você tenha o using acima
        var provider = services.BuildServiceProvider();

        using var server = new BackgroundServer(provider);
        server.AddClashOfClansJobs();

        Console.WriteLine("Jobs agendados. Pressione Ctrl+C para sair.");
        await Task.Delay(-1);
    }
}
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddClashOfClansServices(this IServiceCollection services)
    {
        // Registre aqui todos os serviços, jobs, etc.
        services.AddSingleton<ClashOfClansService>();
        services.AddTransient<BuscarMembrosJob>();

        return services;
    }
}