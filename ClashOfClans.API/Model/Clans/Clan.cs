using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model.Clans;
public class Clan : Entity, IAggregateRoot
{
    public string Tag { get; init; }
    public string Nome { get; init; }
    public List<Membro> Membros { get; private set; } = [];

    private Clan() { }
    public Clan(string tag, string nome)
    {
        if (string.IsNullOrWhiteSpace(tag))
            throw new ArgumentException("Tag não pode ser vazia.", nameof(tag));

        if (!tag.StartsWith('#'))
            throw new ArgumentException("Tag deve começar com #.", nameof(tag));

        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser vazio.", nameof(nome));

        Tag = tag;
        Nome = nome;
    }

    public void AdicionarMembro(string tag, string nome)
    {
        Membro? membro = Membros.FirstOrDefault(m => m.Tag == tag);
        if (membro is not null)
        {
            membro.AlterarNome(nome);
            return;
        }

        Membro novoMembro = new(Id, tag, nome);
        Membros.Add(novoMembro);
    }

    public void InativarMembro(string membroTag)
    {
        var membro = Membros.FirstOrDefault(m => m.Tag == membroTag);
        if (membro is null)
            return;

        membro.InativarMembro();
    }
}
