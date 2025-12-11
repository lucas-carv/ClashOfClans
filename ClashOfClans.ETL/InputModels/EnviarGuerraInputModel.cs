namespace ClashOfClans.ETL.InputModels;

public class EnviarGuerraInputModel
{
    public string Status { get; init; }
    public DateTime InicioGuerra { get; init; }
    public DateTime FimGuerra { get; init; }
    public string TipoGuerra { get; init; }
    public ClanGuerraDTO Clan { get; init; }
}
public class ClanGuerraDTO
{
    public required string Tag { get; init; }
    public required string Nome { get; init; }
    public int ClanLevel { get; set; }
    public IEnumerable<MembroGuerraDTO> Membros { get;  set; } = [];
}

public class MembroGuerraDTO
{
    public required string Tag { get; init; }
    public required string Nome { get; init; }
    public required int CentroVilaLevel { get; set; }
    public IEnumerable<AtaquesDTO> Ataques { get; set; } = [];

}
public class AtaquesDTO
{
    public required string AtacanteTag { get; init; }
    public required string DefensorTag { get; init; }
    public int Estrelas { get; set; }
}
