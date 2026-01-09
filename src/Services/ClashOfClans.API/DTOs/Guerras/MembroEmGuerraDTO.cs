namespace ClashOfClans.API.DTOs.Guerras;

public record MembroEmGuerraDTO
{
    public required string Tag { get; set; }
    public required int CentroVilaLevel { get; set; }
    public required string Nome { get; set; }
    public int PosicaoMapa { get; set; }
    public IEnumerable<AtaquesDTO> Ataques { get; set; } = [];
}
