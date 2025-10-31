using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model.Guerras;

public class ClanEmGuerra : Entity
{
    public string Tag { get; init; }
    public List<MembroEmGuerra> Membros { get; set; } = [];
    public int GuerraId { get; private set; }
    public Guerra Guerra { get; private set; }
    private ClanEmGuerra() { }
    public ClanEmGuerra(string tag)
    {
        Tag = tag;
    }

    public void AdicionarMembro(string tag, string nome)
    {
        MembroEmGuerra membroGuerra = new(tag, nome);
        Membros.Add(membroGuerra);
    }
}