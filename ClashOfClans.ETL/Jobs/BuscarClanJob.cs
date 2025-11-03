using ClashOfClans.ETL.InputModels;
using ClashOfClans.ETL.Models;
using ClashOfClans.ETL.Services;
using ClashOfClans.ETL.Services.Integration;
using Quartz;

namespace ClashOfClans.ETL.Jobs;

public class BuscarClanJob(ClashOfClansService clashOfClansService) : IJob
{
    private readonly ClashOfClansService _clashOfClansService = clashOfClansService;

    public async Task Execute(IJobExecutionContext context)
    {
        string tag = "#2L0UC9R8P";
        string encodedTag = Uri.EscapeDataString(tag);
        Clan clan = await _clashOfClansService.BuscarClan(encodedTag);
        IntegrationService integrationService = new();

        CriarClanInputModel clanInputModel = new()
        {
            Tag = clan.Tag,
            Nome = clan.Name,
            Membros = clan.MemberList.Select(m => new MembroDTO()
            {
                Nome = m.Name,
                Tag = m.Tag
            })
        };

        CriarClanInputModel clanIntegrado = await integrationService.ObterClanPorTag(encodedTag);
        if (clanIntegrado == null)
        {
            await integrationService.CriarClan(clanInputModel);
            return;
        }
        await integrationService.AtualizarClan(clanInputModel);
    }
}
