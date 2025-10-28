using ClashOfClans.ETL.Models;
using ClashOfClans.ETL.Services;
using ClashOfClans.ETL.Services.Integration;
using Quartz;

namespace ClashOfClans.ETL.Jobs;

public class BuscarGuerraJob(ClashOfClansService clashOfClansService) : IJob
{
    private readonly ClashOfClansService _clashOfClansService = clashOfClansService;

    public async Task Execute(IJobExecutionContext context)
    {
        string tag = "#2L0UC9R8P";
        string encodedTag = Uri.EscapeDataString(tag);

        War war = await _clashOfClansService.BuscarGuerra(encodedTag);
        IntegrationService integrationService = new();

        GuerraInputModel guerraInputModel = new()
        {
            Clan = new ClanGuerra()
            {
                Tag = war.Clan.Tag,
                Membros = [.. war.Clan.Members.Select(m => new MembroGuerraDTO()
                {
                    Ataques = m.Attacks.Select(a => new AtaquesDTO() { Estrela = a.Stars }).ToList(),
                    Nome = m.Name!,
                    Tag = m.Tag!
                })],
            },
            FimGuerra = war.EndTime,
            InicioGuerra = war.StartTime,
            Status = war.State.ToString()
        };

        var clanIntegracao = await integrationService.ObterClanPorTag(encodedTag);
        if (clanIntegracao is not null)
        {
            await integrationService.EnviarGuerra(guerraInputModel);
            return;
        }
    }
}

public class GuerraInputModel
{
    public string Status { get; set; } = string.Empty;
    public DateTime InicioGuerra { get; set; }
    public DateTime FimGuerra { get; set; }
    public ClanGuerra Clan { get; set; } = new();
}

public class ClanGuerra
{
    public string Tag { get; set; }
    public List<MembroGuerraDTO> Membros { get; set; } = [];
}

public class MembroGuerraDTO
{
    public string Tag { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public List<AtaquesDTO> Ataques { get; set; } = [];

}
public class AtaquesDTO
{
    public int Estrela { get; set; }
}

