using Newtonsoft.Json;

namespace ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Responses;

public class WarResponse
{
    public StatusGuerra State { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public required DateTime StartTime { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public required DateTime EndTime { get; set; }
    public required ClanWarDTO Clan { get; set; }
}

public enum StatusGuerra
{
    NotInWar,
    WarEnded,
    Preparation,
    InWar
}

public class ClanWarDTO
{
    public required string Tag { get; set; } = string.Empty;
    public required string Name { get; set; } = string.Empty;
    public IEnumerable<MembersWarDTO> Members { get; set; } = [];
}

public class MembersWarDTO
{
    public required string Tag { get; set; } = string.Empty;
    public required string Name { get; set; } = string.Empty;
    public IEnumerable<AttacksDTO> Attacks { get; set; } = [];

}
public class AttacksDTO
{
    public required string AttackerTag { get; set; }
    public required string DefenderTag { get; set; }
    public required int Stars { get; set; }

}