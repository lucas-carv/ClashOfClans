namespace ClashOfClans.API.InputModels
{
    public class ClanInputModel
    {
        public string? Tag { get; set; }
        public string? Name { get; set; }
        public List<MemberInputModel> MemberList { get; set; } = [];
    }
    public class MemberInputModel
    {
        public string? Tag { get; set; }
        public string? Name { get; set; }
    }
}
