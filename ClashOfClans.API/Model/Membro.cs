using ClashOfClans.API.Core;
using System.Text.Json.Serialization;

namespace ClashOfClans.API.Model;

public class Membro(int clanId, string tag, string nome) : Entity
{
    public int ClanId { get; private set; } = clanId;
    public string Tag { get; private set; } = tag;
    public string Nome { get; private set; } = nome;
    public SituacaoMembro Situacao { get; set; } = SituacaoMembro.Ativo;

    public void AlterarNome(string nome)
    {
        Nome = nome;
        Situacao = SituacaoMembro.Ativo;
    }
}
