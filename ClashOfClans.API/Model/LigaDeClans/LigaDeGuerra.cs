using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model.LigaDeClans
{ 

    public class LigaDeGuerra : Entity, IAggregateRoot
    {
        public string Status { get; set; }
        public string Temporada { get; set; }
        public List<LigaGuerraClan>? Clans { get; set; }
        public List<LigaGuerraRodada>? Rodadas { get; set; }
    }

    public class LigaGuerraClan : Entity
    {
        public string Tag { get; set; }
        public int ClanLevel { get; set; }
        public string Nome { get; set; }
        public List<LigaGuerraMembro> Membros { get; set; }
    }
    public class LigaGuerraMembro : Entity
    {
        public string Tag { get; set; }
        public int CentroVilaLevel { get; set; }
        public string Nome { get; set; }
    }
    public class LigaGuerraRodada : Entity
    {
        public List<string> GuerraTags { get; set; }
    }
}
