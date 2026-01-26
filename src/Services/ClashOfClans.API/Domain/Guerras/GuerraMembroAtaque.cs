using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model.Guerras;

public class GuerraMembroAtaque : Entity
{
    public string AtacanteTag { get; init; }
    public string DefensorTag { get; init; }
    public int Estrelas { get; set; }
    public int OrdemAtaque { get; set; }
    public decimal PercentualDestruicao { get; set; }
    public int MembroEmGuerraId { get; }
    public MembroEmGuerra MembroEmGuerra { get; }
    private GuerraMembroAtaque() { }
    public GuerraMembroAtaque(string atacanteTag, string defensorTag, int estrelas, decimal percentualDestruicao, int ordemAtaque)
    {
        AtacanteTag = atacanteTag;
        DefensorTag = defensorTag;
        Estrelas = estrelas;
        PercentualDestruicao = percentualDestruicao;
        OrdemAtaque = ordemAtaque;
    }

    public void AtualizarAtaque(int estrelas, decimal percentualDestruicao, int ordemAtaque)
    {
        Estrelas = estrelas;
        PercentualDestruicao = percentualDestruicao;
        OrdemAtaque = ordemAtaque;
    }
}