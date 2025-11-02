namespace ClashOfClans.ETL.Models;

public record class Clan
{
    public required string Tag { get; init; }
    public required string Name { get; init; }
    public List<ClanMemberList> MemberList { get; set; } = [];
}

public class ClanMemberList
{
    public required string Tag { get; init; }
    public required string Name { get; init; }
}