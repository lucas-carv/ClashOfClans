namespace ClashOfClans.API.DTOs;
public record ClanEmGuerraDTO
{
    public required string Tag { get; init; }
    public string Nome { get; set; }
    public int ClanLevel { get; set; }
    public IEnumerable<MembroEmGuerraDTO> Membros { get; set; } = [];
}
public record MembroEmGuerraDTO
{
    public required string Tag { get; set; }
    public required int CentroVilaLevel { get; set; }
    public required string Nome { get; set; }
    public IEnumerable<AtaquesDTO> Ataques { get; set; } = [];
}
public record AtaquesDTO
{
    public required string AtacanteTag { get; init; }
    public required string DefensorTag { get; init; }
    public int Estrelas { get; set; } = 0;
}
