namespace ClashOfClans.ETL.Models.LigaDeClans;

public class ClanWarLeagueGroup
{
    public string State { get; set; }
    public string Season { get; set; }
    public List<ClanWarLeagueClan>? Clans { get; set; }
    public List<ClanWarLeagueRound>? Rounds { get; set; }
}

public class ClanWarLeagueClan
{
    public string Tag { get; set; }
    public int ClanLevel { get; set; }
    public string Name { get; set; }
    public List<ClanWarLeagueClanMember> Members { get; set; }
}
public class ClanWarLeagueClanMember
{
    public string Tag { get; set; }
    public int TownHallLevel { get; set; }
    public string Name { get; set; }
}
public class ClanWarLeagueRound
{
    public List<string> WarTags { get; set; }
}