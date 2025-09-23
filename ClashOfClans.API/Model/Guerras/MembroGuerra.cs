using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model.Guerras;

public class MembroGuerra : Entity
{
    public int GuerraClanId { get; set; } // FK
    public GuerraClan GuerraClan { get; set; }
    public string Tag { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public List<Ataque> Ataques { get; set; } = [];

    public void AdicionarAtaque(int estrelas)
    {
        Ataque ataque = new()
        {
            MembroId = this.Id,
            Estrelas = estrelas
        };

        Ataques.Add(ataque);
    }
}