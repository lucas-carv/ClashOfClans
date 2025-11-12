using ClashOfClans.ETL.InputModels;
using ClashOfClans.ETL.Models;
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

        War war = await _clashOfClansService.BuscarGuerra(encodedTag);
        if (war.State.Equals(StatusGuerra.NotInWar))
            return;

        EnviarGuerraInputModel guerraInputModel = CriarGuerraInputModel(war);

        var clanIntegracao = await _integrationService.ObterClanPorTag(encodedTag);
        if (clanIntegracao is not null)
        {
            Console.WriteLine($"{DateTime.Now} - Enviando guerra");
            await _integrationService.EnviarGuerra(guerraInputModel);

            Console.WriteLine($"{DateTime.Now} - Guerra enviada com sucesso");
            return;
        }
    }

    private static EnviarGuerraInputModel CriarGuerraInputModel(War war)
    {
        EnviarGuerraInputModel inputModel = new()
        {
            Status = war.State.ToString(),
            InicioGuerra = war.StartTime,
            FimGuerra = war.EndTime,
            Clan = new ClanGuerraDTO()
            {
                Tag = war.Clan.Tag,
                Membros = war.Clan.Members.Select(m => new MembroGuerraDTO()
                {
                    Nome = m.Name,
                    Tag = m.Tag,
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

