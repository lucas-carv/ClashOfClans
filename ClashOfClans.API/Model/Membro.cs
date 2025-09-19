using ClashOfClans.API.Core;

namespace ClashOfClans.API.Model;

public class Membro(int clanId, string tag, string nome) : Entity
{
    public int ClanId { get; private set; } = clanId;
    public string Tag { get; private set; } = tag;
    public string Nome { get; private set; } = nome;
    public SituacaoMembro Situacao { get; set; } = SituacaoMembro.Ativo;

    public void AlterarNome(string nome)
    {
        Nome = nome;
        Situacao = SituacaoMembro.Ativo;
    }
}

//public class League : Entity
//{
//    public int LeagueId { get; set; }
//    public string Name { get; set; } = string.Empty;
//    public IconUrls IconUrls { get; set; } = new();
//}

//public class IconUrls : Entity
//{
//    public string Small { get; set; } = string.Empty;
//    public string Tiny { get; set; } = string.Empty;
//    public string Medium { get; set; } = string.Empty;
//}

//public class PlayerHouse : Entity
//{
//    public List<HouseElement> Elements { get; set; } = [];
//}

//public class HouseElement : Entity
//{
//    public string Type { get; set; } = string.Empty;
//    public int Id { get; set; }
//}

//public class BuilderBaseLeague : Entity
//{
//    public int BuilderBaseLeagueId { get; set; }
//    public string Name { get; set; } = string.Empty;
//}

//public class Label : Entity
//{
//    public int LabelId { get; set; }
//    public string Name { get; set; } = string.Empty;
//    public IconUrls IconUrls { get; set; } = new();
//}

//public class ClanCapital : Entity
//{
//    public int CapitalHallLevel { get; set; }
//    public List<District> Districts { get; set; } = [];
//}

//public class District : Entity
//{
//    public int DistrictId { get; set; }
//    public string Name { get; set; } = string.Empty;
//    public int DistrictHallLevel { get; set; }
//}

//public class ChatLanguage : Entity
//{
//    public int ChatLanguageId { get; set; }
//    public string Name { get; set; } = string.Empty;
//    public string LanguageCode { get; set; } = string.Empty;
//}

//public class Location : Entity
//{
//    public int LocationId { get; set; }
//    public string Name { get; set; } = string.Empty;
//    public bool IsCountry { get; set; }
//}

//public class BadgeUrls : Entity
//{
//    public string Small { get; set; } = string.Empty;
//    public string Large { get; set; } = string.Empty;
//    public string Medium { get; set; } = string.Empty;
//}

//public class CapitalLeague : Entity
//{
//    public int CapitalLeagueId { get; set; }
//    public string Name { get; set; } = string.Empty;
//}

//public class WarLeague : Entity
//{
//    public int WarLeagueId { get; set; }
//    public string Name { get; set; } = string.Empty;
//}
