using ClashOfClans.API.Core;
using ClashOfClans.API.Core.CommandResults;

namespace ClashOfClans.API.Model.Guerras;

public class Guerra : Entity, IAggregateRoot
{
    public string Status { get; set; } = string.Empty;
    public DateTime InicioGuerra { get; init; }
    public DateTime FimGuerra { get; private set; }
    public List<ClanEmGuerra> ClansEmGuerra { get; set; } = [];
    public string TipoGuerra { get; init; } = "Normal";
    public string GuerraTag { get; private set; } = string.Empty;

    private Guerra() { }
    public Guerra(string status, DateTime inicioGuerra, DateTime fimGuerra, string tipoGuerra)
    {
        Status = status;
        InicioGuerra = inicioGuerra;
        FimGuerra = fimGuerra;
        TipoGuerra = tipoGuerra;
    }

    public void AdicionarClan(ClanEmGuerra clan)
    {
        ClansEmGuerra.Add(clan);
    }

    public void DefinirGuerraTag(string guerraTag)
    {
        GuerraTag = guerraTag;
    }

    public void AlterarDataFinalGuerra(DateTime fimGuerra)
    {
        if (InicioGuerra > fimGuerra)
        {
            //Gerar exceção adequada
            throw new("A data de inicio da guerra não pode ser maior que a final");
        }

        FimGuerra = fimGuerra;
    }

    public static Result<Guerra> Criar(string status, DateTime inicioGuerra, DateTime fimGuerra, string tipoGuerra, List<ClanEmGuerra> clans)
    {
        if (inicioGuerra > fimGuerra)
            return ValidationErrors.GuerraValidationErros.InicioMaiorQueFinal;

        Guerra guerra = new(status, inicioGuerra, fimGuerra, tipoGuerra);

        foreach (var clan in clans)
        {
            guerra.AdicionarClan(clan);
        }
        return guerra;
    }
}
