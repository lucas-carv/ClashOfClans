using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model.Guerras;

public class ClanEmGuerra : Entity
{
    public string Tag { get; init; }
    public string Nome { get; set; }
    public List<MembroEmGuerra> MembrosEmGuerra { get; set; } = [];
    public int GuerraId { get; private set; }
    public Guerra Guerra { get; private set; }
    public TipoClanNaGuerra Tipo { get; private set; }
    private ClanEmGuerra() { }
    public ClanEmGuerra(string tag, string nome, TipoClanNaGuerra tipo)
    {
        Tag = tag;
        Nome = nome;
        Tipo = tipo;
    }

    public MembroEmGuerra AdicionarMembro(string tag, string nome, int posicaoMapa)
    {
        var membroExiste = MembrosEmGuerra.FirstOrDefault(m => m.Tag == tag);
        if (membroExiste is not null)
            return membroExiste;

        MembroEmGuerra membroGuerra = new(tag, nome, posicaoMapa);
        MembrosEmGuerra.Add(membroGuerra);
        return membroGuerra;
    }
}
public enum TipoClanNaGuerra
{
    Principal = 1,
    Oponente = 2
}