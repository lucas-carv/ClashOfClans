namespace ClashOfClans.ETL.InputModels;

public record CriarClanInputModel
{
    public required string Tag { get; init; }
    public required string Nome { get; init; }
    public IEnumerable<MembroDTO> Membros { get; set; } = [];
}

public record MembroDTO
{
    public required string Tag { get; init; }
    public required string Nome { get; init; }
}
