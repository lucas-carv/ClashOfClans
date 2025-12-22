namespace ClashOfClans.ETL.Models.LeagueClans;

public record ClanWarLeagueClanMemberDTO
{
    public required string Tag { get; set; }
    public int TownHallLevel { get; set; }
    public required string Name { get; set; }
}
