using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model;

public class MembroGuerraResumo : Entity
{
    public string ClanTag { get; set; } = null!;
    public string Tag { get; set; } = null!;
    public string Nome { get; set; } = null!;
    public int GuerrasParticipadasSeq { get; set; }   
    public int QuantidadeAtaques { get; set; }      
    public int UltimaGuerraId { get; set; }         
}