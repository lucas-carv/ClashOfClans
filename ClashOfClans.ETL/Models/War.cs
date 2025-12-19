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
    public required ClanWar Clan { get; set; }
}
public record ClanWar
{
    public required string Tag { get; set; } 
    public required string Name { get; set; } 
    public List<MembersWarDTO> Members { get; set; } = [];
}

public record MembersWarDTO
{
    public required string Tag { get; set; } 
    public required string Name { get; set; } 
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
