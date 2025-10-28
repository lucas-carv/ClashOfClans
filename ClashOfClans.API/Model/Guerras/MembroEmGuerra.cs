using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model.Guerras;

public class MembroEmGuerra : Entity
{
    public int GuerraClanId { get; set; }
    public string Tag { get; init; }
    public string Nome { get; init; }
    public List<Ataque> Ataques { get; set; } = [];
    private MembroEmGuerra() { }
    public MembroEmGuerra(int guerraClanId, string tag, string nome)
    {
        GuerraClanId = guerraClanId;
        Tag = tag;
        Nome = nome;
    }

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