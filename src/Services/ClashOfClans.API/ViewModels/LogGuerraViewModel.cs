namespace ClashOfClans.API.ViewModels;

public class LogGuerraViewModel
{
    public string Resultado { get; set; }
    public string ClanTag { get; set; }
    public string ClanNome { get; set; }
    public int EstrelasClan { get; set; }
    public string OponenteNome { get; set; }
    public string OponenteTag { get; set; }
    public int EstrelasOponente { get; set; }
    public DateTime InicioGuerra { get; set; } 
    public DateTime FimGuerra { get; set; } 
}
