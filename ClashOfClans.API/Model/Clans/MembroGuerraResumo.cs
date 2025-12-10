using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model.Clans;

public class MembroGuerraResumo : Entity
{
    public string ClanTag { get; init; } 
    public string Tag { get; init; } 
    public string Nome { get; init; }
    public int GuerrasParticipadasSeq { get;  set; } = 0;
    public int QuantidadeAtaques { get;  set; } = 0;

    public MembroGuerraResumo(string clanTag, string tag, string nome)
    {
        ClanTag = clanTag;
        Tag = tag;
        Nome = nome;
    }

    public void AtualizarQuantidadeAtaques(int quantidadeAtaques)
    {
        QuantidadeAtaques = quantidadeAtaques;
    }
}