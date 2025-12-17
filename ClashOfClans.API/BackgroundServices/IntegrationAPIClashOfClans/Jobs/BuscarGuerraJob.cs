using MediatR;
using Quartz;
using ClashOfClans.API.DTOs.Guerras;
using ClashOfClans.API.Application.Commands.Guerras;
using ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Responses;
using ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Services;

namespace ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Jobs;

[DisallowConcurrentExecution]
public class BuscarGuerraJob(ClashOfClansService clashOfClansService, IMediator mediator) : IJob
{
    private readonly ClashOfClansService _clashOfClansService = clashOfClansService;
    private readonly IMediator _mediator = mediator;

    public async Task Execute(IJobExecutionContext context)
    {
        string tag = "#2L0UC9R8P";
        string encodedTag = Uri.EscapeDataString(tag);

        ResponseClashOfClans<WarResponse>? response = await _clashOfClansService.BuscarGuerra(encodedTag);
        if (!response.IsValid)
        {
            Console.WriteLine($"Erro ao obter guerra {string.Join(",", response.Erros)}");
            return;
        }

        WarResponse? war = response.ResponseData;
        if (war is null)
        {
            Console.WriteLine("Guerra não encontrada na api");
            return;
        }

        if (war.State.Equals(StatusGuerra.NotInWar))
            return;

        ClanEmGuerraDTO clan = new()
        {
            ClanLevel = 0,
            Nome = war.Clan.Name,
            Tag = war.Clan.Tag,
            Membros = war.Clan.Members.Select(m => new MembroEmGuerraDTO()
            {
                CentroVilaLevel = 0,
                Nome = m.Name,
                Tag = m.Tag,
                Ataques = m.Attacks.Select(a => new AtaquesDTO()
                {
                    Estrelas = a.Stars,
                    AtacanteTag = a.AttackerTag,
                    DefensorTag = a.DefenderTag
                })
            })
        };

        UpsertGuerraRequest upsertGuerraRequest = new(war.State.ToString(), war.StartTime, war.EndTime, "Normal", clan);

        await _mediator.Send(upsertGuerraRequest);
    }
}
public static class BuscarGuerraJobConfiguration
{
    public static void AddBuscarGuerraJob(this IServiceCollectionQuartzConfigurator configurator)
    {
        JobKey jobKey = new(nameof(BuscarGuerraJob));
        configurator.AddJob<BuscarGuerraJob>(opts => opts.WithIdentity(jobKey));

        configurator.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity($"{nameof(BuscarGuerraJob)}-trigger")
            .WithSimpleSchedule(x => x.WithIntervalInMinutes(5)
            .RepeatForever())
            );
    }
}
