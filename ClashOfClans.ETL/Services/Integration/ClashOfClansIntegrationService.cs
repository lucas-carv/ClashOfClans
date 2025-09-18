using ClashOfClans.ETL.Jobs;
using ClashOfClans.ETL.Models;

namespace ClashOfClans.ETL.Services.Integration;

public class IntegrationService : IntegrationServiceBaseApiService
{


    public async Task<bool> CriarClan(ClanInputModel clan)
    {
        string uri = $"{_baseUrl}/clan/criar";
        var response = await Send<bool, ClanInputModel>(clan, HttpMethod.Post, uri);
        return response.IsValid;
    }
    public async Task<bool> AtualizarClan(ClanInputModel clan)
    {
        string uri = $"{_baseUrl}/clan/atualizar";
        var response = await Send<bool, ClanInputModel>(clan, HttpMethod.Put, uri);
        return response.IsValid;
    }
    

    public async Task<Clan> ObterClanPorTag(string tag)
    {
        string uri = $"https://localhost:7016/api/v1/clan/{tag}";
        var response = await Send<Clan>(HttpMethod.Get, uri);
        return response.ResponseData;
    }
}

public class ResponseIntegrationApi<T>
{
    public bool IsValid { get; set; }

    public T ResponseData { get; set; }

    public string[] Erros { get; set; }
}