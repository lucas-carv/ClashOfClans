using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model.Guerras;

public class Guerra : Entity, IAggregateRoot
{
    public string Status { get; private set; } = string.Empty;
    public DateTime InicioGuerra { get; private set; }
    public DateTime FimGuerra { get; private set; }
    public GuerraClan GuerraClan { get; set; } = new();

    public Guerra()
    {
        
    }
    public Guerra(string status, DateTime inicioGuerra, DateTime fimGuerra, GuerraClan guerraClan)
    {
        Status = status;
        InicioGuerra = inicioGuerra;
        FimGuerra = fimGuerra;
        GuerraClan = guerraClan;
    }
}
