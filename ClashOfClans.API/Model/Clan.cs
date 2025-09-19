using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model;
public class Clan : Entity, IAggregateRoot
{
    public string Tag { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public List<Membro> Membros { get; set; } = [];

    public Clan() { }
    public Clan(string tag, string nome)
    {
        Tag = tag;
        Nome = nome;
    }

    public void AdicionarMembro(string tag, string nome)
    {
        Membro membro = Membros.FirstOrDefault(m => m.Tag == tag)!;
        if (membro is not null)
        {
            membro.AlterarNome(nome);
            return;
        }

        Membro novoMembro = new(this.Id, tag, nome);
        Membros.Add(novoMembro);
    }

    public void InativarMembro(string membroTag)
    {
        var membro = Membros.FirstOrDefault(m => m.Tag == membroTag);
        if (membro is null)
            return;

        membro.Situacao = SituacaoMembro.Inativo;
    }
}
