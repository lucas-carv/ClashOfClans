﻿namespace ClashOfClans.ETL.ClashOfClans.Model;

public class Guerras
{
    public List<Item> Items { get; set; } = [];
}

public class Item
{
    public string Result { get; set; } = string.Empty;
    public string EndTime { get; set; } = string.Empty;
    public int TeamSize { get; set; }
    public int AttacksPerMember { get; set; }
    public string BattleModifier { get; set; } = string.Empty;
    public Clan Clan { get; set; } = new Clan();
    public Opponent Opponent { get; set; } = new Opponent();
}

public class Clan
{
    public string Tag { get; set; }
    public string Name { get; set; }
    public BadgeUrls BadgeUrls { get; set; }
    public int ClanLevel { get; set; }
    public int Attacks { get; set; }
    public int Stars { get; set; }
    public double DestructionPercentage { get; set; }
    public int ExpEarned { get; set; }
}

public class Opponent
{
    public string Tag { get; set; }
    public string Name { get; set; }
    public BadgeUrls BadgeUrls { get; set; }
    public int ClanLevel { get; set; }
    public int Stars { get; set; }
    public double DestructionPercentage { get; set; }
}

public class BadgeUrls
{
    public string Small { get; set; }
    public string Large { get; set; }
    public string Medium { get; set; }
}
