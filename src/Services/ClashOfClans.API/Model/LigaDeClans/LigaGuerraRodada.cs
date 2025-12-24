using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model.LigaDeClans
{
    public class LigaGuerraRodada : Entity
    {
        public int Dia { get; init; }
        public DateTime InicioGuerra { get; private set; }
        public DateTime FimGuerra { get; private set; }
        public string ClanTag { get; private set; }
        public string ClanTagOponente { get; private set; }
        public string Status { get; private set; }
        public string GuerraTag { get; init; }

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

        public void AtualizarRodada(string status, string clanTag, string clanTagOponente, DateTime inicioGuerra, DateTime fimGuerra)
        {
            Status = status;
            ClanTag = clanTag;
            ClanTagOponente = clanTagOponente;
            InicioGuerra = inicioGuerra;
            FimGuerra = fimGuerra;
        }
    }
}
