using ClashOfClans.ETL.InputModels;

namespace ClashOfClans.ETL.Responses;

public record AtualizarClanResponse
{
    public required int Tag { get; set; }
    public required string Nome { get; set; }
    public required IEnumerable<MembroDTO> Membros { get; set; }
}
