namespace ClashOfClans.API.DTOs.Guerras;
public class ClanEmGuerraDTO
{
    public string Tag { get; init; }
    public string Nome { get; set; }
    public int ClanLevel { get; set; }
    public IEnumerable<MembroEmGuerraDTO> Membros { get; set; } = [];

    public ClanEmGuerraDTO()
    {

    }
}
