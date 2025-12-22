namespace ClashOfClans.ETL.Models.LeagueClans;

public record ClanWarLeagueRoundDTO
{
    public IEnumerable<string> WarTags { get; set; } = [];
}
