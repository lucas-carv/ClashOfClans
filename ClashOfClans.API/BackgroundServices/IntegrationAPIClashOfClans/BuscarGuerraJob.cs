using System.Globalization;
using System.Runtime.InteropServices;
using MediatR;
using Quartz;
using Newtonsoft.Json;
using ClashOfClans.API.Application.Commands.Guerras;
using ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Responses;
using ClashOfClans.API.DTOs.Guerras;

namespace ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans;

public class BuscarGuerraJob(ClashOfClansService clashOfClansService, IMediator mediator) : IJob
{
    private readonly ClashOfClansService _clashOfClansService = clashOfClansService;

    private readonly IMediator _mediator = mediator;
    public async Task Execute(IJobExecutionContext context)
    {
        string tag = "#2L0UC9R8P";
        string encodedTag = Uri.EscapeDataString(tag);

        WarResponse war = await _clashOfClansService.BuscarGuerra(encodedTag);
        if (war is null)
        {
            Console.WriteLine($"Guerra não encontrada na API");
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