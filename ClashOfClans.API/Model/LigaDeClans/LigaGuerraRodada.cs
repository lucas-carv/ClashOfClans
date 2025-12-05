using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model.LigaDeClans
{
    public class LigaGuerraRodada : Entity
    {
        public int Dia { get; init; }
        public string GuerraTag { get; init; }
        public string ClanTag { get; private set; }
        public string ClanTagOponente { get; private set; }

        private LigaGuerraRodada() { }

        public LigaGuerraRodada(int dia, string guerraTag, string clanTag, string clanTagOponente)
        {
            Dia = dia;
            GuerraTag = guerraTag;
            ClanTag = clanTag;
            ClanTagOponente = clanTagOponente;
        }

        public void AtualizarRodada(string clanTag, string clanTagOponente)
        {
            ClanTag = ClanTag;
            ClanTagOponente = ClanTagOponente;
        }
    }
}
