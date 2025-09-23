using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model.Guerras;

public class GuerraClan : Entity
{
    public string Tag { get; set; }
    public List<MembroGuerra> Membros { get; set; } = [];
    public int GuerraId { get; set; } // FK
    public Guerra Guerra { get; set; }
    public GuerraClan()
    {

    }
    public GuerraClan(string tag, List<MembroGuerra> membros)
    {
        Tag = tag;
        Membros = membros;
    }

    public void AdicionarMembro(string tag, string nome, IEnumerable<Ataque> ataques)
    {
        MembroGuerra membroGuerra = new()
        {
            Tag = tag,
            Nome = nome
        };

        foreach (var ataqueGuerra in ataques)
        {
            membroGuerra.AdicionarAtaque(ataqueGuerra.Estrelas);
        }

        Membros.Add(membroGuerra);
    }
}