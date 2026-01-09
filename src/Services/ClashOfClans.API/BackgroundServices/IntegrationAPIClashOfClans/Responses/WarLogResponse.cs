using ClashOfClans.API.Common;
using Newtonsoft.Json;

namespace ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Responses;

public class WarLogResponse
{
    public List<WarLogResponseEntry> Items { get; set; }
}

public class ClanWarLogDTO
{
    public string Tag { get; set; }
    public string Name { get; set; }
    public int ClanLevel { get; set; }
    public int Attacks { get; set; }
    public int Stars { get; set; }
    public decimal DestructionPercentage { get; set; }
    public int ExpEarned { get; set; }
}
public class OpponentWarLogDTO
{
    public string Tag { get; set; }
    public string Name { get; set; }
    public int ClanLevel { get; set; }
    public int Stars { get; set; }
    public decimal DestructionPercentage { get; set; }
}
public class WarLogResponseEntry
{
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime EndTime { get; set; }
    public string Result { get; set; }
    public int TeamSize { get; set; }
    public int AttacksPerMember { get; set; }
    public string BattleModifier { get; set; }

    public ClanWarLogDTO Clan { get; set; }
    public OpponentWarLogDTO Opponent { get; set; }
}
