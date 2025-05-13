namespace ClashOfClans.Integracao.API.Model;

public class Historico
{
    public string NomeRecurso { get; set; } = string.Empty;
    public string ReferenciaId { get; set; } = string.Empty;
    public DateTime DataImportacao { get; set; } = DateTime.Parse("2021-01-01 00:00:00");
}
