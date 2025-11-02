using ClashOfClans.ETL.InputModels;
using ClashOfClans.ETL.Jobs;
using ClashOfClans.ETL.Models;

namespace ClashOfClans.ETL.Services.Integration;

public class IntegrationService : IntegrationServiceBaseApiService
{
    public async Task<bool> CriarClan(CriarClanInputModel clan)
    {
        string uri = $"{_baseUrl}/clan/criar";
        var response = await Send<bool, CriarClanInputModel>(clan, HttpMethod.Post, uri);
        return response.IsValid;
    }
    public async Task<bool> AtualizarClan(CriarClanInputModel clan)
    {
        string uri = $"{_baseUrl}/clan/atualizar";
        var response = await Send<bool, CriarClanInputModel>(clan, HttpMethod.Put, uri);
        return response.IsValid;
    }
    
    public async Task<CriarClanInputModel> ObterClanPorTag(string tag)
    {
        string uri = $"https://localhost:7016/api/v1/clan/{tag}";
        var response = await Send<CriarClanInputModel>(HttpMethod.Get, uri);
        return response.ResponseData;
    }

    public async Task<bool> EnviarGuerra(EnviarGuerraInputModel guerra)
    {
        string uri = $"{_baseUrl}/guerra/criar";
        var response = await Send<bool, EnviarGuerraInputModel>(guerra, HttpMethod.Put, uri);
        return response.IsValid;
    }
}

public class ResponseIntegrationApi<T>
{
    public bool IsValid { get; set; }
    public T ResponseData { get; set; }
    public string[] Erros { get; set; }
}