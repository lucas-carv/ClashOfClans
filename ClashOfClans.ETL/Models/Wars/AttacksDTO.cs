namespace ClashOfClans.ETL.Models.Wars;

public record AttacksDTO
{
    public required string AttackerTag { get; set; }
    public required string DefenderTag { get; set; }
    public required int Stars { get; set; }
    public required decimal DestructionPercentage { get; set; }
}
