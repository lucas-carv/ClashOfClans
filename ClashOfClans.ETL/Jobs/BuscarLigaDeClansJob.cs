using ClashOfClans.ETL.InputModels;
using ClashOfClans.ETL.Models.LigaDeClans;
using ClashOfClans.ETL.Services;
using ClashOfClans.ETL.Services.Integration;
using Quartz;

namespace ClashOfClans.ETL.Jobs;

public class BuscarLigaDeClansJob(ClashOfClansService clashOfClansService) : IJob
{

    private readonly ClashOfClansService _clashOfClansService = clashOfClansService;
    public async Task Execute(IJobExecutionContext context)
    {
        string tag = "#2L0UC9R8P";
        string encodedTag = Uri.EscapeDataString(tag);

        ClanWarLeagueGroup clanWarLeague = await _clashOfClansService.BuscarLiga(encodedTag);
        if (clanWarLeague is null)
        {
            Console.WriteLine("Falha ao obter clan");
            return;
        }
        IntegrationService integrationService = new();

        LigaDeGuerra ligaDeGuerra = new()
        {
            Status = clanWarLeague.State,
            Temporada = clanWarLeague.Season,
            Rodadas = clanWarLeague.Rounds.Select(r => new LigaGuerraRodada()
            {
                GuerraTags = r.WarTags
            }).ToList(),
            Clans = clanWarLeague.Clans.Select(c => new LigaGuerraClan()
            {
                ClanLevel = c.ClanLevel,
                Nome = c.Name,
                Tag = c.Tag,

                Membros = c.Members.Select(m => new LigaGuerraMembro()
                {
                    CentroVilaLevel = m.TownHallLevel,
                    Nome = m.Name,
                    Tag = m.Tag
                }).ToList(),
            }).ToList()
        };

        await integrationService.EnviarLigaDeClan(ligaDeGuerra);

        //CriarClanInputModel clanInputModel = new()
        //{
        //    Tag = clan.Tag,
        //    Nome = clan.Name,
        //    Membros = clan.MemberList.Select(m => new MembroDTO()
        //    {
        //        Tag = m.Tag,
        //        Nome = m.Name
        //    })
        //};

        //CriarClanInputModel clanIntegrado = await integrationService.ObterClanPorTag(encodedTag);
        //if (clanIntegrado is not null)
        //{
        //    Console.WriteLine($"{DateTime.Now} - Atualizando Clan");
        //    await integrationService.AtualizarClan(clanInputModel);
        //    return;
        //}

        //Console.WriteLine($"{DateTime.Now} - Criando Clan");
        //await integrationService.CriarClan(clanInputModel);
    }
}
public class LigaDeGuerra
{
    public string Status { get; set; }
    public string Temporada { get; set; }
    public List<LigaGuerraClan>? Clans { get; set; }
    public List<LigaGuerraRodada>? Rodadas { get; set; }
}

public class LigaGuerraClan
{
    public string Tag { get; set; }
    public int ClanLevel { get; set; }
    public string Nome { get; set; }
    public List<LigaGuerraMembro> Membros { get; set; }
}
public class LigaGuerraMembro
{
    public string Tag { get; set; }
    public int CentroVilaLevel { get; set; }
    public string Nome { get; set; }
}
public class LigaGuerraRodada
{
    public List<string> GuerraTags { get; set; }
}