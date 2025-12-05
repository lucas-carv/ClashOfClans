using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model.LigaDeClans
{

    public class LigaDeGuerra : Entity, IAggregateRoot
    {
        public string ClanTag { get; init; }
        public string Status { get; init; }
        public string Temporada { get; init; }
        public IReadOnlyCollection<LigaGuerraRodada> Rodadas => _rodadas;
        private readonly List<LigaGuerraRodada> _rodadas = [];
        public IReadOnlyCollection<LigaGuerraClan> Clans => _clans;
        private readonly List<LigaGuerraClan> _clans = [];
        private LigaDeGuerra() { }
        public LigaDeGuerra(string clanTag, string status, string temporada)
        {
            ClanTag = clanTag;
            Status = status;
            Temporada = temporada;
        }

        public LigaGuerraClan AdicionarClan(string tag, string nome, int clanLevel)
        {
            LigaGuerraClan? clanExiste = Clans.FirstOrDefault(c => c.Tag == tag);
            if (clanExiste is not null)
                return clanExiste;

            LigaGuerraClan ligaGuerraClan = new(tag, nome, clanLevel);
            _clans.Add(ligaGuerraClan);

            return ligaGuerraClan;
        }
        public void AdicionarRodada(string status, int dia, string guerraTag, string clanTag, string clanTagOponente, DateTime inicioGuerra, DateTime fimGuerra)
        {
            LigaGuerraRodada? rodadaExiste = Rodadas.FirstOrDefault(c => c.GuerraTag == guerraTag && c.Dia == dia);
            if (rodadaExiste is not null)
            {
                rodadaExiste.AtualizarRodada(clanTag, clanTagOponente, inicioGuerra, fimGuerra);
                return;
            }

            LigaGuerraRodada novaRodada = new(status, dia, guerraTag, clanTag, clanTagOponente, inicioGuerra, fimGuerra);
            _rodadas.Add(novaRodada);
        }
    }
}
