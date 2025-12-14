namespace ClashOfClans.API.DTOs.Guerras;

public record AtaquesDTO
{
    public required string AtacanteTag { get; init; }
    public required string DefensorTag { get; init; }
    public int Estrelas { get; set; } = 0;
}
