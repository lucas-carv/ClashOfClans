using ClashOfClans.API.Core;
using System.Text.Json.Serialization;

namespace ClashOfClans.API.Model;

public class Membro(int clanId, string tag, string nome) : Entity
{
    public int ClanId { get; init; } = clanId;
    public string Tag { get; init; } = tag;
    public string Nome { get; private set; } = nome;
    //public DateTime DataEntrada { get; set; } = DateTime.Now;
    public SituacaoMembro Situacao { get; private set; } = SituacaoMembro.Ativo;

    public void AlterarNome(string nome)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nome);

        Nome = nome;
        Situacao = SituacaoMembro.Ativo;
    }

    public void InativarMembro()
    {
        Situacao = SituacaoMembro.Inativo;
    }
}
