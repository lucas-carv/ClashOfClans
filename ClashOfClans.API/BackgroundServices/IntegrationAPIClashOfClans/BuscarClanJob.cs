using Quartz;
using ClashOfClans.API.Data;
using ClashOfClans.API.Application.Commands.Clans;
using ClashOfClans.API.Core.CommandResults;
using MediatR;
using ClashOfClans.API.DTOs;
using ClashOfClans.API.Model.Clans;
using Microsoft.EntityFrameworkCore;
using ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Responses;
using ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Services;

namespace ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans;

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
        if (clanClashResponse.ResponseData is null)
        {
            Console.WriteLine($"Falha ao obter clan {string.Join(",", clanClashResponse.Erros)}");
            return;
        }

        Clan? clan = await _context.Clans.FirstOrDefaultAsync(c => c.Tag == tag);

        if (clan is not null)
        {
            var request = new AtualizarClanRequest(clanClashResponse.ResponseData.Tag, clanClashResponse.ResponseData.Name, clanClashResponse.ResponseData.MemberList.Select(m => new MembroClanDTO() { Nome = m.Name, Tag = m.Tag }));

            CommandResult<AtualizarClanResponse> resultadoAtualizacao = await _mediator.Send(request);
            return;
        }

        Console.WriteLine($"{DateTime.Now} - Criando Clan");
        CriarClanRequest clanInputModel = new(clanClashResponse.ResponseData.Tag, clanClashResponse.ResponseData.Name, clanClashResponse.ResponseData.MemberList.Select(c => new MembroClanDTO() { Nome = c.Name, Tag = c.Tag }));
        CommandResult<CriarClanResponse> resultadoCriacao = await _mediator.Send(clanInputModel);
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