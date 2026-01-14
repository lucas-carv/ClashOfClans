using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model.LigaDeClans
{
    public class LigaGuerraMembro : Entity
    {
        public string Tag { get; init; }
        public string Nome { get; init; }
        public int CentroVilaLevel { get; private set; }
        public LigaGuerraMembro(string tag, string nome, int centroVilaLevel)
        {
            Tag = tag;
            Nome = nome;
            CentroVilaLevel = centroVilaLevel;
        }

        public void AtualizarLevelCentroDeVila(int centroVilaLevel)
        {
            if (centroVilaLevel <= 0)
                throw new Exception("Centro de vila informado incorretamente");
            CentroVilaLevel = centroVilaLevel;
        }
    }
}
