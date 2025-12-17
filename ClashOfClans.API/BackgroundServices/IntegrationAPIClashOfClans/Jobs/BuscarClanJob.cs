using MediatR;
using Quartz;
using Microsoft.EntityFrameworkCore;
using ClashOfClans.API.Data;
using ClashOfClans.API.Application.Commands.Clans;
using ClashOfClans.API.DTOs;
using ClashOfClans.API.Model.Clans;
using ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Responses;
using ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Services;

namespace ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Jobs;

[DisallowConcurrentExecution]
public class BuscarClanJob(ClashOfClansService clashOfClansService, ClashOfClansContext context, IMediator mediator) : IJob
{
    private readonly ClashOfClansService _clashOfClansService = clashOfClansService;
    private readonly ClashOfClansContext _context = context;

    private readonly IMediator _mediator = mediator;

    public async Task Execute(IJobExecutionContext context)
    {
        string tag = "#2L0UC9R8P";
        string encodedTag = Uri.EscapeDataString(tag);

        ResponseClashOfClans<ClanResponse> clanClashResponse = await _clashOfClansService.BuscarClan(encodedTag);
        if (!clanClashResponse.IsValid)
        {
            Console.WriteLine($"Falha ao obter clan da API {string.Join(",", clanClashResponse.Erros)}");
            return;
        }

        ClanResponse? clanResponse = clanClashResponse.ResponseData;
        if (clanResponse is null)
        {
            Console.WriteLine($"Clan não encontrado");
            return;
        }

        Clan? clan = await _context.Clans.FirstOrDefaultAsync(c => c.Tag == tag);
        if (clan is not null)
        {
            Console.WriteLine($"{DateTime.Now} - Atualizando Clan");
            AtualizarClanRequest atualizarClan = new(
                clanResponse.Tag,
                clanResponse.Name,
                clanResponse.MemberList
                    .Select(m =>
                        new MembroClanDTO()
                        {
                            Nome = m.Name,
                            Tag = m.Tag
                        }));

            await _mediator.Send(atualizarClan);
            return;
        }

        Console.WriteLine($"{DateTime.Now} - Criando Clan");
        CriarClanRequest criarClan = new(
            clanResponse.Tag,
            clanResponse.Name,
            clanResponse.MemberList
                .Select(c =>
                    new MembroClanDTO()
                    {
                        Nome = c.Name,
                        Tag = c.Tag
                    }));

        await _mediator.Send(criarClan);
    }
}
public static class BuscarClanJobConfiguration
{
    public static void AddBuscarClanJob(this IServiceCollectionQuartzConfigurator configurator)
    {
        JobKey jobKey = new(nameof(BuscarClanJob));
        configurator.AddJob<BuscarClanJob>(opts => opts.WithIdentity(jobKey));

        configurator.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity($"{nameof(BuscarClanJob)}-trigger")
            .StartAt(DateBuilder.FutureDate(1, IntervalUnit.Minute))
            .WithSimpleSchedule(x => x.WithIntervalInMinutes(5)
            .RepeatForever())
            );
    }
}

public class ClanWarLeagueGroup
{
    public string State { get; set; }
    public string Season { get; set; }
    public List<ClanWarLeagueClan>? Clans { get; set; }
    public List<ClanWarLeagueRound>? Rounds { get; set; }
}

public class ClanWarLeagueClan
{
    public string Tag { get; set; }
    public int ClanLevel { get; set; }
    public string Name { get; set; }
    public List<ClanWarLeagueClanMember> Members { get; set; }
}
public class ClanWarLeagueClanMember
{
    public string Tag { get; set; }
    public int TownHallLevel { get; set; }
    public string Name { get; set; }
}
public class ClanWarLeagueRound
{
    public List<string> WarTags { get; set; }
}

public class ClanWarLeague
{
    //public string State { get; set; }
    //[JsonConverter(typeof(CustomDateTimeConverter))]
    //public DateTime StartTime { get; set; }
    //[JsonConverter(typeof(CustomDateTimeConverter))]
    //public DateTime EndTime { get; set; }
    //public ClanWar Clan { get; set; }
    //public ClanWar Opponent { get; set; }
}