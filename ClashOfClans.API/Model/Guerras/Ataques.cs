using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model.Guerras;

public class Ataque : Entity
{
    public string AtacanteTag { get; init; }
    public string DefensorTag { get; init; }
    public int Estrelas { get; private set; } = 0;
    private Ataque() { }
    public Ataque(string atacanteTag, string defensorTag)
    {
        AtacanteTag = atacanteTag;
        DefensorTag = defensorTag;
    }

    public void AtualizarEstrelas(int estrelas)
    {
        Estrelas = estrelas;
    }
}