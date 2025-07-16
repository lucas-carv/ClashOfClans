namespace ClashOfClans.ETL.Models;

public class Clan
{
    public string? Tag { get; set; }
    public string? Name { get; set; }
    public List<Membro> MemberList { get; set; } = [];
}

public class Membro
{
    public string? Tag { get; set; }
    public string? Name { get; set; }
}