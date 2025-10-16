namespace ClashOfClans.API.Core.CommandResults;

public static class ValidationErrors
{
    public static class Guerra
    {
        public static readonly ErrorMessage InicioMaiorQueFinal = new("INICIO_MAIOR_QUE_FINAL", "O horário de início da guerra não pode ser maior que o final");
    }
}
