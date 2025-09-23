namespace ClashOfClans.API.InputModels.Guerras;

public class GuerraInputModel
{
    public string Status { get; set; } = string.Empty;
    public DateTime InicioGuerra { get; set; }
    public DateTime FimGuerra { get; set; }
    public ClanGuerraDTO Clan { get; set; } = new();
}

public class ClanGuerraDTO
{
    public string Tag { get; set; } = string.Empty;
    public List<ParticipantesGuerraDTO> Membros { get; set; } = [];
}
public class ParticipantesGuerraDTO
{
    public string Tag { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public List<AtaquesDTO> Ataques { get; set; } = [];

}
public class AtaquesDTO
{
    public int Estrelas { get; set; }
}