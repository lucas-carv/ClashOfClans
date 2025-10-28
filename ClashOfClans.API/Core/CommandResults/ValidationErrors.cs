namespace ClashOfClans.API.Core.CommandResults;

public static class ValidationErrors
{
    public static class Guerra
    {
        public static readonly ErrorMessage InicioMaiorQueFinal = new("INICIO_MAIOR_QUE_FINAL", "O horário de início da guerra não pode ser maior que o final");
    }
    public static class Clan
    {
        public static readonly ErrorMessage ClanNaoExiste = new("CLAN_NAO_EXISTE", "O Clan com a tag informada não foi encontrado");
        public static readonly ErrorMessage ClanJaExiste = new("CLAN_JA_CADASTRADO", "O Clan com a tag informada já foi cadastrado na base");
    }
}
