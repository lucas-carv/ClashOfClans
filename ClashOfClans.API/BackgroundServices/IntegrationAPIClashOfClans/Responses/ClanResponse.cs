namespace ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Responses;

public record ClanResponse
{
    public required string Tag { get; init; }
    public required string Name { get; init; }
    public IEnumerable<MemberListDTO> MemberList { get; set; } = [];
}

public record MemberListDTO
{
    public required string Tag { get; init; }
    public required string Name { get; init; }
}
