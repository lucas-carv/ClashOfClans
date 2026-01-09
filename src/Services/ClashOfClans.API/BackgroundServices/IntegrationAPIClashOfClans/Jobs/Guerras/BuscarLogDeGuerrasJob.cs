using ClashOfClans.API.Application.Commands.Clans;
using ClashOfClans.API.Application.Commands.Guerras;
using ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Responses;
using ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Services;
using ClashOfClans.API.Data;
using ClashOfClans.API.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Jobs.Guerras;


[DisallowConcurrentExecution]
public class BuscarLogDeGuerrasJob(ClashOfClansService clashOfClansService, ClashOfClansContext context, IMediator mediator) : IJob
{
    private readonly ClashOfClansService _clashOfClansService = clashOfClansService;
    private readonly ClashOfClansContext _context = context;
    private readonly IMediator _mediator = mediator;
    public async Task Execute(IJobExecutionContext context)
    {
        string tag = "#2L0UC9R8P";
        string encodedTag = Uri.EscapeDataString(tag);

        var logResponse = await _clashOfClansService.BuscarWarLog(encodedTag);
        if (!logResponse.IsValid)
        {
            Console.WriteLine($"Falha ao obter clan da API {string.Join(",", logResponse.Erros)}");
            return;
        }

        var warLogResponse = logResponse.ResponseData;
        if (warLogResponse is null)
        {
            Console.WriteLine($"Nenhum log encontrado");
            return;
        }
        var request = warLogResponse.Items.Select(l => new WarLogResponseEntryDTO
        {
            AtaquesPorMembro = l.AttacksPerMember,
            FimGuerra = l.EndTime,
            ModificadorDeBatalha = l.BattleModifier,
            QuantidadeMembros = l.TeamSize,
            Resultado = l.Result,
            ClanWarLog = new ClanLogDTO()
            {
                QuantidadeAtaques = l.Clan.Attacks,
                ClanLevel = l.Clan.ClanLevel,
                Estrelas = l.Clan.Stars,
                ExpGanho = l.Clan.ExpEarned,
                Nome = l.Clan.Name,
                PorcentagemDestruicao = l.Clan.DestructionPercentage,
                Tag = l.Clan.Tag
            },
            OpponenteWarLog = new OponenteLogDTO()
            {
                ClanLevel = l.Opponent.ClanLevel,
                Estrelas = l.Opponent.Stars,
                Nome = l.Opponent.Name ?? string.Empty,
                PorcentagemDestruicao = l.Opponent.DestructionPercentage,
                Tag = l.Opponent.Tag
            }
        }).ToList();
        UpsertWarLogRequest warLogRequest = new(request);

        await _mediator.Send(warLogRequest);
    }
}

public static class AnalisarGuerrasJobConfiguration
{
    public static void AddObterWarLog(this IServiceCollectionQuartzConfigurator configurator)
    {
        JobKey jobKey = new(nameof(BuscarLogDeGuerrasJob));
        configurator.AddJob<BuscarLogDeGuerrasJob>(opts => opts.WithIdentity(jobKey));

        configurator.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity($"{nameof(BuscarLogDeGuerrasJob)}-trigger")
            .StartNow()
            //.StartAt(DateBuilder.FutureDate(1, IntervalUnit.Minute))
            .WithSimpleSchedule(x => x.WithIntervalInHours(10)
            .RepeatForever())
            );
    }
}
