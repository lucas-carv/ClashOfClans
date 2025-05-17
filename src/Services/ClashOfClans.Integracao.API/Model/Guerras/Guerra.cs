using ClashOfClans.Integracao.API.Core;

namespace ClashOfClans.Integracao.API.Model.Guerras;


public class Guerra : Entity, IAggregateRoot
{
    public Guerra()
    {
    }

    public Guerra(string result, DateTime endTime, int teamSize, int attacksPerMember, string battleModifier, Clan clan, Opponent opponent)
    {
        Result = result;
        EndTime = endTime;
        TeamSize = teamSize;
        AttacksPerMember = attacksPerMember;
        BattleModifier = battleModifier;
        Clan = clan;
        Opponent = opponent;
    }

    public string Result { get; set; } = string.Empty;
    public DateTime EndTime { get; set; }
    public int TeamSize { get; set; }
    public int AttacksPerMember { get; set; }
    public string BattleModifier { get; set; } = string.Empty;
    public Clan Clan { get; set; } = new();
    public Opponent Opponent { get; set; } = new();
}
