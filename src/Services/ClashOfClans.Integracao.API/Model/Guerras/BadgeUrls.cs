namespace ClashOfClans.Integracao.API.Model.Guerras;

public class BadgeUrls
{
    public int Id { get; set; }
    public string Small { get; set; } = string.Empty;
    public string Large { get; set; } = string.Empty;
    public string Medium { get; set; } = string.Empty;
    public string ClanTag { get; set; }
    public Clan Clan { get; set; } 
    public string OpponentTag { get; set; } = string.Empty;

    public Opponent Opponent { get; set; }
}
