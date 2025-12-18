using ClashOfClans.ETL.Common;
using Newtonsoft.Json;

namespace ClashOfClans.ETL.Models;

public record War
{
    public StatusGuerra State { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime StartTime { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime EndTime { get; set; }
    public ClanWar Clan { get; set; } = new();
}
public record ClanWar
{
    public string Tag { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<MembersWarDTO> Members { get; set; } = [];
}

public record MembersWarDTO
{
    public string Tag { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<AttacksDTO> Attacks { get; set; } = [];

}
public record AttacksDTO
{
    public required string AttackerTag { get; set; }
    public required string DefenderTag { get; set; }
    public int Stars { get; set; }
}
public enum StatusGuerra
{
    NotInWar,
    WarEnded,
    Preparation,
    InWar
}
