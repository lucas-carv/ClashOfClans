namespace ClashOfClans.ETL.Models.Wars;

public record MembersWarDTO
{
    public required string Tag { get; set; } 
    public required string Name { get; set; } 
    public int TownhallLevel { get; set; }
    public int MapPosition { get; set; }
    public IEnumerable<AttacksDTO> Attacks { get; set; } = [];
}
