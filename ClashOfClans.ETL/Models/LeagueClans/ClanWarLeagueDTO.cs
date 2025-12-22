using ClashOfClans.ETL.Common;
using ClashOfClans.ETL.Models.Wars;
using Newtonsoft.Json;

namespace ClashOfClans.ETL.Models.LeagueClans;

public record ClanWarLeagueDTO
{
    public required string State { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime StartTime { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime EndTime { get; set; }
    public required ClanWarDTO Clan { get; set; }
    public required ClanWarDTO Opponent { get; set; }
}