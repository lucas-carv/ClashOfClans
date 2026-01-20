namespace ClashOfClans.API.ViewModels;

public class DesempenhoMembroViewModel
{
    public required string MembroTag { get; set; }
    public required string Nome { get; set; }
    public int TotalAtaques { get; set; }
    public int TotalEstrelas { get; set; }
    public double MediaEstrelas { get; set; }
    public double MediaDestruicao { get; set; }
    public int QuantidadeGuerras { get; set; }
}