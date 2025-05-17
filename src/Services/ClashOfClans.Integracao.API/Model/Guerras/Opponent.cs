namespace ClashOfClans.Integracao.API.Model.Guerras;

public class Opponent
{
    public string Tag { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public BadgeUrls BadgeUrls { get; set; } = new();
    public int ClanLevel { get; set; }
    public int Stars { get; set; }
    public double DestructionPercentage { get; set; }
}
