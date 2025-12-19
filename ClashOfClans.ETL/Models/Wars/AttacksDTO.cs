namespace ClashOfClans.ETL.Models.Wars;

public record AttacksDTO
{
    public required string AttackerTag { get; set; }
    public required string DefenderTag { get; set; }
    public int Stars { get; set; }
}
