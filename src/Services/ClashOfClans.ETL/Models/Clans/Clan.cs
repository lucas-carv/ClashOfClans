namespace ClashOfClans.ETL.Models.Clans;

public record Clan
{
    public required string Tag { get; init; }
    public required string Name { get; init; }
    public IEnumerable<ClanMemberList> MemberList { get; set; } = [];
}
