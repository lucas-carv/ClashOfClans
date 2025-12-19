namespace ClashOfClans.ETL.Models.Wars;

public record ClanWarDTO
{
    public required string Tag { get; set; } 
    public required string Name { get; set; } 
    public List<MembersWarDTO> Members { get; set; } = [];
}
