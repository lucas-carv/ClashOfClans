namespace ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Responses;

public record ClashClanResponse
{
    public required string Tag { get; init; }
    public required string Name { get; init; }
    public IEnumerable<ClanMemberListDTO> MemberList { get; set; } = [];
}

public record ClanMemberListDTO
{
    public required string Tag { get; init; }
    public required string Name { get; init; }
}
