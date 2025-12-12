using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model.Clans;

public class Membro(int clanId, string tag, string nome) : Entity
{
    public int ClanId { get; init; } = clanId;
    public string Tag { get; init; } = tag;
    public string Nome { get; private set; } = nome;
    public DateTime DataEntrada { get; private set; } = HorarioBrasil.Agora;
    public SituacaoMembro Situacao { get; private set; } = SituacaoMembro.Ativo;

    public void AtualizarDataEntradaENome(string nome)
    {
        ArgumentException.ThrowIfNullOrEmpty(nome);

        Nome = nome;
        Situacao = SituacaoMembro.Ativo;
        DataEntrada = HorarioBrasil.Agora;
    }

    public void InativarMembro()
    {
        Situacao = SituacaoMembro.Inativo;
    }
}
