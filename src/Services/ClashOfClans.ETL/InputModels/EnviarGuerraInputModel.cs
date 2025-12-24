namespace ClashOfClans.ETL.InputModels;

public record EnviarGuerraInputModel
{
    public required string Status { get; init; }
    public DateTime InicioGuerra { get; init; }
    public DateTime FimGuerra { get; init; }
    public required string TipoGuerra { get; init; }
    public required ClanGuerraDTO Clan { get; init; }
}
public record ClanGuerraDTO
{
    public required string Tag { get; init; }
    public required string Nome { get; init; }
    public int ClanLevel { get; set; }
    public IEnumerable<MembroGuerraDTO> Membros { get; set; } = [];
}

public record MembroGuerraDTO
{
    public required string Tag { get; init; }
    public required string Nome { get; init; }
    public required int CentroVilaLevel { get; set; }
    public required int PosicaoMapa { get; set; }
    public IEnumerable<AtaquesDTO> Ataques { get; set; } = [];

}
public record AtaquesDTO
{
    public required string AtacanteTag { get; init; }
    public required string DefensorTag { get; init; }
    public int Estrelas { get; set; }
    public decimal PercentualDestruicao { get; set; }
}
