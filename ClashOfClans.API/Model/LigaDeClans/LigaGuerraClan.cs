using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model.LigaDeClans
{
    public class LigaGuerraClan : Entity
    {
        public string Tag { get; init; }
        public string Nome { get; init; }
        public int ClanLevel { get; private set; }
        public IReadOnlyCollection<LigaGuerraMembro> Membros => _membros;
        private readonly List<LigaGuerraMembro> _membros = [];
        private LigaGuerraClan() { }
        public LigaGuerraClan(string tag, string nome, int clanLevel)
        {
            Tag = tag;
            Nome = nome;
            ClanLevel = clanLevel;
        }

        public void AtualizarLevelClan(int clanLevel)
        {
            if (clanLevel <= 0)
                throw new Exception("Level do clan informado incorretamente");

            ClanLevel = clanLevel;
        }

        public void AdicionarMembro(string tag, string nome, int centroVilaLevel)
        {
            LigaGuerraMembro? membro = Membros.FirstOrDefault(m => m.Tag == tag && m.Nome == nome);
            if (membro is not null)
            {
                membro.AtualizarLevelCentroDeVila(centroVilaLevel);
                return;
            }

            var novoMembro = new LigaGuerraMembro(tag, nome, centroVilaLevel);
            _membros.Add(novoMembro);
            return;
        }
    }
}
