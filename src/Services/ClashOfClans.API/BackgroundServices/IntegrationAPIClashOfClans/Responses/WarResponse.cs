using ClashOfClans.API.Common;
using Newtonsoft.Json;

namespace ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Responses;

public record WarResponse
{
    public StatusGuerra State { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public required DateTime StartTime { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public required DateTime EndTime { get; set; }
    public required ClanWarDTO Clan { get; set; }
    public required ClanWarDTO Opponent { get; set; }
}
public record ClanWarDTO
{
    public int ClanLevel { get; set; }
    public required string Tag { get; set; } 
    public required string Name { get; set; }
    public int Attacks { get; set; }
    public int Stars { get; set; }
    public decimal DestructionPercentage { get; set; }
    public IEnumerable<MembersWarDTO> Members { get; set; } = [];
}

public record MembersWarDTO
{
    public required string Tag { get; set; }
    public required string Name { get; set; } 
    public IEnumerable<AttacksDTO> Attacks { get; set; } = [];
    public required int MapPosition { get; set; }
    public required int TownhallLevel { get; set; }

}
public record AttacksDTO
{
    public required string AttackerTag { get; set; }
    public required string DefenderTag { get; set; }
    public required int Stars { get; set; }
    public required decimal DestructionPercentage { get; set; }
    public int Order { get; set; }
}
public enum StatusGuerra
{
    NotInWar,
    WarEnded,
    Preparation,
    InWar
}
