namespace ClashOfClans.API.DTOs.Guerras;

public record AtaquesDTO
{
    public required string AtacanteTag { get; init; }
    public required string DefensorTag { get; init; }
    public required int Estrelas { get; set; }
    public required decimal PercentualDestruicao { get; set; }
    public required int OrdemAtaque { get; set; }
}
