using ClashOfClans.API.Core;
using ClashOfClans.API.Core.CommandResults;

namespace ClashOfClans.API.Model.Guerras;

public class Guerra : Entity, IAggregateRoot
{
    public string Status { get; set; } = string.Empty;
    public DateTime InicioGuerra { get; init; }
    public DateTime FimGuerra { get; init; }
    public ClanEmGuerra ClanEmGuerra { get; init; }

    private Guerra(){}
    public Guerra(string status, DateTime inicioGuerra, DateTime fimGuerra, ClanEmGuerra clanEmGuerra)
    {
        Status = status;
        InicioGuerra = inicioGuerra;
        FimGuerra = fimGuerra;
        ClanEmGuerra = clanEmGuerra;
    }

    public static Result<Guerra> Criar(string status, DateTime inicioGuerra, DateTime fimGuerra, ClanEmGuerra clanEmGuerra)
    {
        ArgumentNullException.ThrowIfNull(clanEmGuerra);

        if (inicioGuerra > fimGuerra)
        {
            return ValidationErrors.Guerra.InicioMaiorQueFinal;
        }

        Guerra guerra = new(status, inicioGuerra, fimGuerra, clanEmGuerra);

        return guerra;

    }
}
