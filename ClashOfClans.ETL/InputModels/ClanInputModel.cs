namespace ClashOfClans.ETL.InputModels;

public class CriarClanInputModel
{
    public required string Tag { get; init; }
    public required string Nome { get; init; }
    public IEnumerable<MembroDTO> Membros { get; set; } = [];
}

public class MembroDTO
{
    public required string Tag { get; init; }
    public required string Nome { get; init; }
}
