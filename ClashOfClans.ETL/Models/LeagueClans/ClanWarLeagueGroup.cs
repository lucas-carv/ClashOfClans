namespace ClashOfClans.ETL.Models.LeagueClans;

public record ClanWarLeagueGroupResponse
{
    public required string State { get; set; }
    public required string Season { get; set; }
    public IEnumerable<ClanWarLeagueClanDTO> Clans { get; set; } = [];
    public IEnumerable<ClanWarLeagueRoundDTO> Rounds { get; set; } = [];
}
