namespace ClashOfClans.Models;
public class ClanViewModel
{
    public string Tag { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<Member> MemberList { get; set; } = [];
}
public class Clan 
{
    public string Tag { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Location Location { get; set; } = new();
    public bool IsFamilyFriendly { get; set; }
    public BadgeUrls BadgeUrls { get; set; } = new();
    public int ClanLevel { get; set; }
    public int ClanPoints { get; set; }
    public int ClanBuilderBasePoints { get; set; }
    public int ClanCapitalPoints { get; set; }
    public CapitalLeague CapitalLeague { get; set; } = new();
    public int RequiredTrophies { get; set; }
    public string WarFrequency { get; set; } = string.Empty;
    public int WarWinStreak { get; set; }
    public int WarWins { get; set; }
    public int WarTies { get; set; }
    public int WarLosses { get; set; }
    public bool IsWarLogPublic { get; set; }
    public WarLeague WarLeague { get; set; } = new();
    public int Members { get; set; }
    public List<Member> MemberList { get; set; } = [];
    public List<Label> Labels { get; set; } = [];
    public int RequiredBuilderBaseTrophies { get; set; }
    public int RequiredTownhallLevel { get; set; }
    public ClanCapital ClanCapital { get; set; } = new();
    public ChatLanguage ChatLanguage { get; set; } = new();
}

public class Location
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsCountry { get; set; }
}

public class BadgeUrls
{
    public string Small { get; set; } = string.Empty;
    public string Large { get; set; } = string.Empty;
    public string Medium { get; set; } = string.Empty;
}

public class CapitalLeague
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class WarLeague
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class Member
{
    public string Tag { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public int TownHallLevel { get; set; }
    public int ExpLevel { get; set; }
    public League League { get; set; } = new();
    public int Trophies { get; set; }
    public int BuilderBaseTrophies { get; set; }
    public int ClanRank { get; set; }
    public int PreviousClanRank { get; set; }
    public int Donations { get; set; }
    public int DonationsReceived { get; set; }
    public PlayerHouse PlayerHouse { get; set; } = new();
    public BuilderBaseLeague BuilderBaseLeague { get; set; } = new();
}

public class League
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IconUrls IconUrls { get; set; } = new();
}

public class IconUrls
{
    public string Small { get; set; } = string.Empty;
    public string Tiny { get; set; } = string.Empty;
    public string Medium { get; set; } = string.Empty;
}

public class PlayerHouse
{
    public List<HouseElement> Elements { get; set; } = [];
}

public class HouseElement
{
    public string Type { get; set; } = string.Empty;
    public int Id { get; set; }
}

public class BuilderBaseLeague
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class Label
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IconUrls IconUrls { get; set; } = new();
}

public class ClanCapital
{
    public int CapitalHallLevel { get; set; }
    public List<District> Districts { get; set; } = [];
}

public class District
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DistrictHallLevel { get; set; }
}

public class ChatLanguage
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LanguageCode { get; set; } = string.Empty;
}
