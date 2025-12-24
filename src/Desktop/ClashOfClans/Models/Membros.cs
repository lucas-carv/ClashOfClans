namespace ClashOfClans.Models;

public record MembroViewModel
{
    public string Nome { get; set; }
    public string Tag { get; set; }

    public int GuerrasParticipadasSeq { get; set; }
    public int QuantidadeAtaques { get; set; }
}