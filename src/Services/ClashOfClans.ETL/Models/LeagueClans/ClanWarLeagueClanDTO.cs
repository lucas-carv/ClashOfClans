namespace ClashOfClans.ETL.Models.LeagueClans;

public record ClanWarLeagueClanDTO
{
    public required string Tag { get; set; }
    public int ClanLevel { get; set; }
    public required string Name { get; set; }
    public IEnumerable<ClanWarLeagueClanMemberDTO> Members { get; set; } = [];
}
