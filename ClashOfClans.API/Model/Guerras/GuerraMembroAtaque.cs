using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model.Guerras;

public class GuerraMembroAtaque : Entity
{
    public string AtacanteTag { get; init; }
    public string DefensorTag { get; init; }
    public int Estrelas { get; set; }
    public int MembroEmGuerraId { get; }
    public MembroEmGuerra MembroEmGuerra { get; }
    // posição mapa atacante
    // posição mapa defensor
    private GuerraMembroAtaque() { }
    public GuerraMembroAtaque(string atacanteTag, string defensorTag)
    {
        AtacanteTag = atacanteTag;
        DefensorTag = defensorTag;
    }

    public void AtualizarEstrelas(int estrelas)
    {
        Estrelas = estrelas;
    }
}