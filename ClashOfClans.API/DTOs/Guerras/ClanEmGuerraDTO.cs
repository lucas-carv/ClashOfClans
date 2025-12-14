namespace ClashOfClans.API.DTOs.Guerras;
public record ClanEmGuerraDTO
{
    public required string Tag { get; init; }
    public required string Nome { get; set; }
    public int ClanLevel { get; set; }
    public IEnumerable<MembroEmGuerraDTO> Membros { get; set; } = [];
}
