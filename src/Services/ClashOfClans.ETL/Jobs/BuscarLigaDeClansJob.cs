using ClashOfClans.ETL.InputModels;
using ClashOfClans.ETL.Models.LeagueClans;
using ClashOfClans.ETL.Services;
using ClashOfClans.ETL.Services.Integration;
using Quartz;

namespace ClashOfClans.ETL.Jobs;

[DisallowConcurrentExecution]
public class BuscarLigaDeClansJob(ClashOfClansService clashOfClansService) : IJob
{
    private readonly ClashOfClansService _clashOfClansService = clashOfClansService;
    public async Task Execute(IJobExecutionContext context)
    {
        string tag = "#2L0UC9R8P";
        string encodedTag = Uri.EscapeDataString(tag);

        ClanWarLeagueGroupResponse clanWarLeagueGroup = await _clashOfClansService.BuscarGrupoLiga(encodedTag);
        if (clanWarLeagueGroup is null)
        {
            Console.WriteLine("Falha ao obter grupo da liga");
            return;
        }
        IntegrationService integrationService = new();
        List<LigaGuerraRodada> rodadas = [];
        List<AtaquesDTO> ataques = new();
        int dia = 1;

        foreach (var rounds in clanWarLeagueGroup.Rounds)
        {
            foreach (var round in rounds.WarTags)
            {
                string encodedWarTag = Uri.EscapeDataString(round);
                ClanWarLeagueDTO clanWarLeague = await _clashOfClansService.BuscarGuerraDaLiga(encodedWarTag);
                if (clanWarLeague.Clan is null)
                {
                    continue;
                }
                LigaGuerraRodada rodada = new()
                {
                    Status = clanWarLeague.State,
                    ClanTag = clanWarLeague.Clan.Tag,
                    ClanTagOponente = clanWarLeague.Opponent.Tag,
                    Dia = dia,
                    GuerraTag = round,
                    TipoGuerra = "Liga",
                    InicioGuerra = clanWarLeague.StartTime,
                    FimGuerra = clanWarLeague.EndTime
                };
                rodadas.Add(rodada);

                foreach (var membro in clanWarLeague.Clan.Members)
                {
                    foreach (var ataque in membro.Attacks)
                    {
                        AtaquesDTO ataquesDTO = new()
                        {
                            AtacanteTag = ataque.AttackerTag,
                            DefensorTag = ataque.DefenderTag,
                            Estrelas = ataque.Stars
                        };
                        ataques.Add(ataquesDTO);
                    }
                }
            }
            dia++;
        }

        LigaDeGuerra ligaDeGuerra = new()
        {
            ClanTag = tag,
            Status = clanWarLeagueGroup.State,
            Temporada = clanWarLeagueGroup.Season,
            Rodadas = rodadas,
            Clans = clanWarLeagueGroup.Clans.Select(c => new ClanEmGuerraDTO()
            {
                ClanLevel = c.ClanLevel,
                Nome = c.Name,
                Tag = c.Tag,

                Membros = c.Members.Select(m => new MembroEmGuerraDTO()
                {
                    CentroVilaLevel = m.TownHallLevel,
                    Nome = m.Name,
                    Tag = m.Tag,
                    Ataques = ataques.Where(a => a.AtacanteTag == m.Tag)
                }).ToList(),
            }).ToList()
        };

        await integrationService.EnviarLigaDeClan(ligaDeGuerra);

    }
}
public class LigaDeGuerra
{
    public string ClanTag { get; set; }
    public string Status { get; set; }
    public string Temporada { get; set; }
    public List<ClanEmGuerraDTO>? Clans { get; set; }
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
    public string Status { get; set; }
    public int Dia { get; set; }
    public string GuerraTag { get; set; }
    public string ClanTag { get; set; }
    public string ClanTagOponente { get; set; }
    public DateTime InicioGuerra { get; set; }
    public DateTime FimGuerra { get; set; }
    public string TipoGuerra { get; set; }
}