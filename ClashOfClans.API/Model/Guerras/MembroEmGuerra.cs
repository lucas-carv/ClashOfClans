using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model.Guerras;

public class MembroEmGuerra : Entity
{
    public string Tag { get; init; }
    public string Nome { get; init; }
    public List<GuerraMembroAtaque> Ataques { get; set; } = [];
    private MembroEmGuerra() { }
    public MembroEmGuerra(string tag, string nome)
    {
        Tag = tag;
        Nome = nome;
    }

    public void AtualizarAtaque(string atacanteTag, string defensorTag, int estrelas)
    {
        GuerraMembroAtaque? ataque = Ataques.FirstOrDefault(a => a.AtacanteTag == atacanteTag && a.DefensorTag == defensorTag);
        if (ataque is not null)
        {
            ataque.AtualizarEstrelas(estrelas);
            return;
        }

        GuerraMembroAtaque novoAtaque = new(atacanteTag, defensorTag);
        novoAtaque.AtualizarEstrelas(estrelas);
        Ataques.Add(novoAtaque);
        return;
    }
}