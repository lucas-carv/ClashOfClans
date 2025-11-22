using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model.Guerras;

public class ClanEmGuerra : Entity
{
    public string Tag { get; init; }
    public List<MembroEmGuerra> MembrosEmGuerra { get; set; } = [];
    public int GuerraId { get; }
    public Guerra Guerra { get; }
    private ClanEmGuerra() { }
    public ClanEmGuerra(string tag)
    {
        Tag = tag;
    }

    public MembroEmGuerra AdicionarMembro(string tag, string nome)
    {
        var membroExiste = MembrosEmGuerra.FirstOrDefault(m => m.Tag == tag);
        if (membroExiste is not null)
            return membroExiste;

        MembroEmGuerra membroGuerra = new(tag, nome);
        MembrosEmGuerra.Add(membroGuerra);
        return membroGuerra;
    }
}