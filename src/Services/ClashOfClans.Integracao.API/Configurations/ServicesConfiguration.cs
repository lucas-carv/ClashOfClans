using ClashOfClans.Integracao.API.Core;
using FluentValidation;
using MediatR;

namespace ClashOfClans.Integracao.API.Configurations;

public static class ServicesConfiguration
{
    public static void AddServicesConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = AppDomain.CurrentDomain.Load("Automatiza.Cloud.GestaoMetas.Application");

        AssemblyScanner
            .FindValidatorsInAssembly(assembly)
            .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));
        services.AddMediatR(assembly);

        services.AddScoped<IMediatorHandler, MediatorHandler>();
    }
}