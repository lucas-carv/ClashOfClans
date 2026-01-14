namespace ClashOfClans.API.Model;

public class MembroGuerraResumo 
{
    public string ClanTag { get; init; } 
    public string MembroTag { get; init; } 
    public string Nome { get; init; }
    public int GuerrasParticipadasSeq { get;  set; } = 0;
    public int QuantidadeAtaques { get;  set; } = 0;

    public MembroGuerraResumo(string clanTag, string membroTag, string nome)
    {
        ClanTag = clanTag;
        MembroTag = membroTag;
        Nome = nome;
    }

    public void AtualizarQuantidadeAtaques(int quantidadeAtaques)
    {
        QuantidadeAtaques = quantidadeAtaques;
    }
}
