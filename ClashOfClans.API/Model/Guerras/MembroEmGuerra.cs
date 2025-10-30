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

    public void AtualizarAtaque(int estrelas)
    {
        Ataque? ataque = Ataques.FirstOrDefault(a => a.MembroId == this.Id);
        if (ataque is not null)
        {
            ataque.Estrelas = estrelas;
            return;
        }

        Ataque novoAtaque = new()
        {
            MembroId = this.Id,
            Estrelas = estrelas
        };
        Ataques.Add(novoAtaque);
        return;
    }
}