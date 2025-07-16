using ClashOfClans.API.Core;
using ClashOfClans.API.InputModels;

namespace ClashOfClans.API.Application.Clans;

public class AdicionarClanCommand : Command<CommandResponse<bool>>
{
    public string Tag { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public List<MemberInputModel> MemberList { get; set; } = [];

    //public string Type { get; set; } = string.Empty;
    //public string Description { get; set; } = string.Empty;
    //public bool IsFamilyFriendly { get; set; }
    //public int ClanLevel { get; set; }
    //public int ClanPoints { get; set; }
    //public int ClanBuilderBasePoints { get; set; }
    //public int ClanCapitalPoints { get; set; }
    //public int RequiredTrophies { get; set; }
    //public string WarFrequency { get; set; } = string.Empty;
    //public int WarWinStreak { get; set; }
    //public int WarWins { get; set; }
    //public int WarTies { get; set; }
    //public int WarLosses { get; set; }
    //public bool IsWarLogPublic { get; set; }
    //public int Members { get; set; }
    //public int RequiredBuilderBaseTrophies { get; set; }
    //public int RequiredTownhallLevel { get; set; }

}
