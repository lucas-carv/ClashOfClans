using ClashOfClans.ETL.InputModels;

namespace ClashOfClans.ETL.Responses;

public record AtualizarClanResponse
{
    public int Tag { get; set; }
    public string Nome { get; set; }
    public IEnumerable<MembroDTO> Membros { get; set; }
}
