using ClashOfClans.ETL.Models;
using ClashOfClans.ETL.Services;
using ClashOfClans.ETL.Services.Integration;
using Quartz;

namespace ClashOfClans.ETL.Jobs;

public class BuscarMembrosJob : IJob
{
    private readonly ClashOfClansService _clashOfClansService;
    public BuscarMembrosJob(ClashOfClansService clashOfClansService)
    {
        _clashOfClansService = clashOfClansService;
    }
    public async Task Execute(IJobExecutionContext context)
    {
        string tag = "#2L0UC9R8P";
        string encodedTag = Uri.EscapeDataString(tag);
        Clan clan = await _clashOfClansService.BuscarClan(encodedTag);
        IntegrationService integrationService = new();

        ClanInputModel clanInputModel = new()
        {
            Tag = clan.Tag!,
            Nome = clan.Name!,
            Membros = clan.MemberList.Select(m => new MembroDTO() { Nome = m.Name, Tag = m.Tag }).ToList()
        };

        var clanIntegracao = await integrationService.ObterClanPorTag(encodedTag);
        if (clanIntegracao == null)
        {
          
            await integrationService.CriarClan(clanInputModel);
            return;
        }
        await integrationService.AtualizarClan(clanInputModel);
    }
}
public class ClanInputModel
{
    public string Tag { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public List<MembroDTO> Membros { get; set; } = [];
}

public class MembroDTO
{
    public string Tag { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
}

