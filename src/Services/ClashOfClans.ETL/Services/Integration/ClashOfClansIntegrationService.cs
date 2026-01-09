using ClashOfClans.ETL.InputModels;
using ClashOfClans.ETL.Jobs;
using ClashOfClans.ETL.Responses;

namespace ClashOfClans.ETL.Services.Integration;

public class IntegrationService : IntegrationServiceBaseApi
{
    public async Task<CriarClanInputModel> CriarClan(CriarClanInputModel clan)
    {
        string uri = $"{_baseUrl}/clan/criar";
        var response = await Send<CriarClanInputModel, CriarClanInputModel>(clan, HttpMethod.Post, uri);
        return response.ResponseData;
    }
    public async Task<bool> AtualizarClan(CriarClanInputModel clan)
    {
        string uri = $"{_baseUrl}/clan/atualizar";
        var response = await Send<AtualizarClanResponse, CriarClanInputModel>(clan, HttpMethod.Put, uri);
        return response.IsValid;
    }

    public async Task<ResponseIntegrationApi<CriarClanInputModel>> ObterClanPorTag(string tag)
    {
        string uri = $"{_baseUrl}/clan/{tag}";
        ResponseIntegrationApi<CriarClanInputModel> response = await Send<CriarClanInputModel>(HttpMethod.Get, uri);
        return response;
    }

    public async Task<ResponseIntegrationApi<UpsertGuerraResponse>> EnviarGuerra(EnviarGuerraInputModel guerra)
    {
        string uri = $"{_baseUrl}/guerra/criar";
        var response = await Send<UpsertGuerraResponse, EnviarGuerraInputModel>(guerra, HttpMethod.Put, uri);
        return response;
    }

    public async Task<bool> EnviarLigaDeClan(LigaDeGuerra ligaDeGuerra)
    {
        string uri = $"{_baseUrl}/liga-guerra/criar";
        var response = await Send<bool, LigaDeGuerra>(ligaDeGuerra, HttpMethod.Post, uri);
        return response.ResponseData;
    }

}
public record UpsertGuerraResponse(string Status, DateTime InicioGuerra, DateTime FimGuerra, ClanEmGuerraDTO Clan);
public record ClanEmGuerraDTO
{
    public required string Tag { get; init; }
    public string Nome { get; set; }
    public int ClanLevel { get; set; }
    public IEnumerable<MembroEmGuerraDTO> Membros { get; set; } = [];
}
public record MembroEmGuerraDTO
{
    public required string Tag { get; set; }
    public required string Nome { get; set; }
    public required int CentroVilaLevel { get; set; }
    public int PosicaoMapa { get; set; }
    public IEnumerable<AtaquesDTO> Ataques { get; set; } = [];
}

public class ResponseIntegrationApi<T>
{
    public bool IsValid { get; set; }
    public T ResponseData { get; set; }
    public string[] Erros { get; set; }
}