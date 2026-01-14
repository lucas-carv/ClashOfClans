namespace ClashOfClans.API.Model;

public class MembroInativoGuerra(string membroTag, string nome, string clanTag, DateTime dataAvaliacao, DateTime dataEntradaMembro)
{
    public string MembroTag { get; init; } = membroTag;
    public string Nome { get; init; } = nome;
    public string ClanTag { get; init; } = clanTag;
    public DateTime DataAvaliacao { get; init; } = dataAvaliacao;
    public DateTime DataEntradaMembro { get; init; } = dataEntradaMembro;
}
