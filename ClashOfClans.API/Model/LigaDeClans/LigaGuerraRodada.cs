using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model.LigaDeClans
{
    public class LigaGuerraRodada : Entity
    {
        public string Status { get; init; }
        public int Dia { get; init; }
        public string GuerraTag { get; init; }
        public string ClanTag { get; private set; }
        public string ClanTagOponente { get; private set; }
        public DateTime InicioGuerra { get; private set; }
        public DateTime FimGuerra { get; private set; }

        private LigaGuerraRodada() { }

        public LigaGuerraRodada(string status, int dia, string guerraTag, string clanTag, string clanTagOponente, DateTime inicioGuerra, DateTime fimGuerra)
        {
            Status = status;
            Dia = dia;
            GuerraTag = guerraTag;
            ClanTag = clanTag;
            ClanTagOponente = clanTagOponente;
            FimGuerra = fimGuerra;
            InicioGuerra = inicioGuerra;
        }

        public void AtualizarRodada(string clanTag, string clanTagOponente, DateTime inicioGuerra, DateTime fimGuerra)
        {
            ClanTag = clanTag;
            ClanTagOponente = clanTagOponente;
            InicioGuerra = inicioGuerra;
            FimGuerra = fimGuerra;
        }
    }
}
