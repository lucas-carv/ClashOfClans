namespace ClashOfClans.ETL.Models.Wars;

public record MembersWarDTO
{
    public required string Tag { get; set; } 
    public required string Name { get; set; } 
    public List<AttacksDTO> Attacks { get; set; } = [];

}
