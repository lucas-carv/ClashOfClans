namespace ClashOfClans.API.ViewModels;

public class ClanViewModel
{
    public string? Tag { get; set; }
    public string? Name { get; set; }
    public List<MemberViewModel> MemberList { get; set; } = [];
}
public class MemberViewModel
{
    public string? Tag { get; set; }
    public string? Name { get; set; }
}
