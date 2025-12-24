namespace ClashOfClans.ETL.Models.Clans;

public record ClanMemberList
{
    public required string Tag { get; init; }
    public required string Name { get; init; }
}