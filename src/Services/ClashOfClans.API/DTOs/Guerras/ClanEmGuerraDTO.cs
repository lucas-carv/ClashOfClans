namespace ClashOfClans.API.DTOs.Guerras;
public class ClanEmGuerraDTO
{
    public string Tag { get; set; }
    public string Nome { get; set; }
    public int ClanLevel { get; set; }
    public IEnumerable<MembroEmGuerraDTO> Membros { get; set; } = [];
    public TipoClanGuerra Tipo { get; set; }
    public int QuantidadeAtaques { get; set; }
    public int Estrelas { get; set; }
    public decimal PercentualDestruicao { get; set; }
}

public enum TipoClanGuerra
{
    Principal = 1,
    Oponente = 2
}