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
        public void AdicionarRodada(int dia, string guerraTag, string clanTag, string clanTagOponente)
        {
            LigaGuerraRodada? rodadaExiste = Rodadas.FirstOrDefault(c => c.GuerraTag == guerraTag && c.Dia == dia);
            if (rodadaExiste is not null)
            {
                rodadaExiste.AtualizarRodada(clanTag, clanTagOponente);
                return;
            }

            LigaGuerraRodada novaRodada = new(dia, guerraTag, clanTag, clanTagOponente);
            _rodadas.Add(novaRodada);
        }
    }
}
