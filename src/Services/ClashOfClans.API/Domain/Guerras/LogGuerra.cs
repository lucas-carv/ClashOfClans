using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model.Guerras;

public class LogGuerra : Entity, IAggregateRoot
{
    public string Resultado { get; set; }
    public DateTime FimGuerra { get; set; }
    public int QuantidadeMembros { get; set; }
    public int AtaquesPorMembro { get; set; }
    public string ModificadorDeBatalha { get; set; }
    public LogClanGuerra Clan { get; set; }
    public LogOponenteGuerra Oponente { get; set; }

    public LogGuerra(string resultado, DateTime fimGuerra, int quantidadeMembros, int ataquesPorMembro, string modificadorDeBatalha)
    {
        Resultado = resultado;
        FimGuerra = fimGuerra;
        QuantidadeMembros = quantidadeMembros;
        AtaquesPorMembro = ataquesPorMembro;
        ModificadorDeBatalha = modificadorDeBatalha;
    }

    public void AdicionarClan(string tag, string nome, int clanLevel, int quantidadeAtaques, int estrelas, decimal porcentagemDestruicao, int expGanho)
    {
        LogClanGuerra logClanGuerra = new(tag, nome, clanLevel, quantidadeAtaques, estrelas, porcentagemDestruicao, expGanho);
        Clan = logClanGuerra;
    }

    public void AdicionarOponente(string tag, string nome, int clanLevel, int estrelas, decimal porcentagemDestruicao)
    {
        LogOponenteGuerra oponente = new(tag, nome, clanLevel, estrelas, porcentagemDestruicao);
        Oponente = oponente;
    }

}

public class LogClanGuerra : Entity
{
    public int LogGuerraId { get; set; }
    public string Tag { get; set; }
    public string Nome { get; set; }
    public int ClanLevel { get; set; }
    public int QuantidadeAtaques { get; set; }
    public int Estrelas { get; set; }
    public decimal PorcentagemDestruicao { get; set; }
    public int ExpGanho { get; set; }
    public LogClanGuerra()
    {
    }
    public LogClanGuerra(string tag, string nome, int clanLevel, int quantidadeAtaques, int estrelas, decimal porcentagemDestruicao, int expGanho)
    {
        Tag = tag;
        Nome = nome;
        ClanLevel = clanLevel;
        QuantidadeAtaques = quantidadeAtaques;
        Estrelas = estrelas;
        PorcentagemDestruicao = porcentagemDestruicao;
        ExpGanho = expGanho;
    }
}
public class LogOponenteGuerra : Entity
{
    public int LogGuerraId { get; set; }
    public string Tag { get; set; }
    public string Nome { get; set; }
    public int ClanLevel { get; set; }
    public int Estrelas { get; set; }
    public decimal PorcentagemDestruicao { get; set; }
    public LogOponenteGuerra()
    {
    }
    public LogOponenteGuerra(string tag, string nome, int clanLevel, int estrelas, decimal porcentagemDestruicao)
    {
        Tag = tag;
        Nome = nome;
        ClanLevel = clanLevel;
        Estrelas = estrelas;
        PorcentagemDestruicao = porcentagemDestruicao;
    }
}
