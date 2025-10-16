using ClashOfClans.API.Core;
using ClashOfClans.API.Core.CommandResults;

namespace ClashOfClans.API.Model.Guerras;

public class Guerra : Entity, IAggregateRoot
{
    public string Status { get; private set; } = string.Empty;
    public DateTime InicioGuerra { get; init; }
    public DateTime FimGuerra { get; init; }
    public GuerraClan GuerraClan { get; set; } = new();

    public Guerra()
    {

    }
    private Guerra(string status, DateTime inicioGuerra, DateTime fimGuerra, GuerraClan guerraClan)
    {
        Status = status;
        InicioGuerra = inicioGuerra;
        FimGuerra = fimGuerra;
        GuerraClan = guerraClan;
    }

    public static Result<Guerra> Criar(string status, DateTime inicioGuerra, DateTime fimGuerra, GuerraClan guerraClan)
    {
        ArgumentNullException.ThrowIfNull(guerraClan);

        if (inicioGuerra > fimGuerra)
        {
            return ValidationErrors.Guerra.InicioMaiorQueFinal;
        }

        Guerra guerra = new(status, inicioGuerra, fimGuerra, guerraClan);

        return guerra;

    }
}
