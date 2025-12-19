using ClashOfClans.ETL.Common;
using Newtonsoft.Json;

namespace ClashOfClans.ETL.Models.Wars;

public record WarResponse
{
    public StatusGuerra State { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime StartTime { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime EndTime { get; set; }
    public required ClanWarDTO Clan { get; set; }
}
public enum StatusGuerra
{
    NotInWar,
    WarEnded,
    Preparation,
    InWar
}
