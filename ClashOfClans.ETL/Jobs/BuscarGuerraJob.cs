using ClashOfClans.ETL.InputModels;
using ClashOfClans.ETL.Models.Wars;
using ClashOfClans.ETL.Services;
using ClashOfClans.ETL.Services.Integration;
using Quartz;

namespace ClashOfClans.ETL.Jobs;

public class EnviarGuerraJob(ClashOfClansService clashOfClansService, IntegrationService integrationService) : IJob
{
    private readonly ClashOfClansService _clashOfClansService = clashOfClansService;
    private readonly IntegrationService _integrationService = integrationService;

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

        EnviarGuerraInputModel guerraInputModel = CriarGuerraInputModel(war);

        var clanIntegracao = await _integrationService.ObterClanPorTag(encodedTag);
        if (clanIntegracao is null)
        {
            Console.WriteLine("Clan ainda não integrado");
            return;
        }

        Console.WriteLine($"{DateTime.Now} - Enviando guerra");
        UpsertGuerraResponse result = await _integrationService.EnviarGuerra(guerraInputModel);
        if (result is null)
        {
            Console.WriteLine($"{DateTime.Now} - Falha no processo de upsert de guerra");
            return;
        }
        Console.WriteLine($"{DateTime.Now} - Guerra enviada com sucesso");
    }

    private static EnviarGuerraInputModel CriarGuerraInputModel(WarResponse war)
    {
        EnviarGuerraInputModel inputModel = new()
        {
            Status = war.State.ToString(),
            InicioGuerra = war.StartTime,
            FimGuerra = war.EndTime,
            TipoGuerra = "Normal",
            Clan = new ClanGuerraDTO()
            {
                Nome = war.Clan.Name,
                Tag = war.Clan.Tag,
                Membros = war.Clan.Members.Select(m => new MembroGuerraDTO()
                {
                    Nome = m.Name,
                    Tag = m.Tag,
                    CentroVilaLevel = 0,
                    Ataques = m.Attacks.Select(a => new AtaquesDTO()
                    {
                        Estrelas = a.Stars,
                        AtacanteTag = a.AttackerTag,
                        DefensorTag = a.DefenderTag
                    })
                }),
            }
        };

        return inputModel;
    }
}

