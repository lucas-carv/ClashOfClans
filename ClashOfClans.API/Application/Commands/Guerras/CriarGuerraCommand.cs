using System.Text.Json.Serialization;

namespace ClashOfClans.API.Application.Commands.Guerras;

public class CriarGuerraCommand
{
    public string Status { get; set; } = string.Empty;
    public DateTime InicioGuerra { get; set; }
    public DateTime FimGuerra { get; set; }
    public ClanGuerraDTO Clan { get; set; } = new();
}
public class ClanGuerraDTO
{

    public List<MembroGuerraDTO> Membros { get; set; } = [];
}

public class MembroGuerraDTO
{
    public string Tag { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public List<AtaquesDTO> Ataques { get; set; } = [];

}
public class AtaquesDTO
{
    public int Estrela { get; set; }

}