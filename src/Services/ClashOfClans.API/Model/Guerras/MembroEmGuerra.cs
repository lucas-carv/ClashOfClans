using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model.Guerras;

public class MembroEmGuerra : Entity
{
    public string Tag { get; init; }
    public string Nome { get; init; }
    public int PosicaoMapa { get; init; }
    public int CentroVilaLevel { get; set; }
    public int ClanEmGuerraId { get; set; }
    public ClanEmGuerra ClanEmGuerra { get; set; }
    public List<GuerraMembroAtaque> Ataques { get; set; } = [];
    private MembroEmGuerra() { }
    public MembroEmGuerra(string tag, string nome, int posicaoMapa)
    {
        Tag = tag;
        Nome = nome;
        PosicaoMapa = posicaoMapa;
    }

    public void AtualizarAtaque(string atacanteTag, string defensorTag, int estrelas, decimal percentualDestruicao)
    {
        GuerraMembroAtaque? ataque = Ataques.FirstOrDefault(a => a.AtacanteTag == atacanteTag && a.DefensorTag == defensorTag);
        if (ataque is not null)
        {
            ataque.AtualizarAtaque(estrelas, percentualDestruicao);
            return;
        }

        GuerraMembroAtaque novoAtaque = new(atacanteTag, defensorTag);
        novoAtaque.AtualizarAtaque(estrelas, percentualDestruicao);
        Ataques.Add(novoAtaque);
        return;
    }
}