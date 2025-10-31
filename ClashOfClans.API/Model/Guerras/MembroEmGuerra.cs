using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model.Guerras;

public class MembroEmGuerra : Entity
{
    public string Tag { get; init; }
    public string Nome { get; init; }
    public List<Ataque> Ataques { get; set; } = [];
    private MembroEmGuerra() { }
    public MembroEmGuerra(string tag, string nome)
    {
        Tag = tag;
        Nome = nome;
    }

    public void AtualizarAtaque(string atacanteTag, string defensorTag, int estrelas)
    {
        Ataque? ataque = Ataques.FirstOrDefault(a => a.DefensorTag == defensorTag && a.AtacanteTag == defensorTag);
        if (ataque is not null)
        {
            ataque.AtualizarEstrelas(estrelas);
            return;
        }

        Ataque novoAtaque = new(atacanteTag, defensorTag);
        novoAtaque.AtualizarEstrelas(estrelas);
        Ataques.Add(novoAtaque);
        return;
    }
}