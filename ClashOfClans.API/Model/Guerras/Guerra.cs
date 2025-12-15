using ClashOfClans.API.Core;
using ClashOfClans.API.Core.CommandResults;

namespace ClashOfClans.API.Model.Guerras;

public class Guerra : Entity, IAggregateRoot
{
    public string Status { get; set; } = string.Empty;
    public DateTime InicioGuerra { get; init; }
    public DateTime FimGuerra { get; private set; }
    public int ClanEmGuerraId { get; set; }
    public ClanEmGuerra ClanEmGuerra { get; set; }
    public string TipoGuerra { get; init; } = "Normal";
    public string GuerraTag { get; private set; } = string.Empty;
    //public ClanEmGuerra ClanEmGuerraOponente { get; init; }

    private Guerra() { }
    public Guerra(string status, DateTime inicioGuerra, DateTime fimGuerra, string tipoGuerra, ClanEmGuerra clanEmGuerra)
    {
        Status = status;
        InicioGuerra = inicioGuerra;
        FimGuerra = fimGuerra;
        TipoGuerra = tipoGuerra;
        ClanEmGuerra = clanEmGuerra;
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

    public static Result<Guerra> Criar(string status, DateTime inicioGuerra, DateTime fimGuerra, string tipoGuerra, ClanEmGuerra clanEmGuerra)
    {
        ArgumentNullException.ThrowIfNull(clanEmGuerra);
        if (inicioGuerra > fimGuerra)
            return ValidationErrors.Guerra.InicioMaiorQueFinal;

        Guerra guerra = new(status, inicioGuerra, fimGuerra, tipoGuerra, clanEmGuerra);
        return guerra;
    }
}
