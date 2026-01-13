using ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Responses;

namespace ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Services;
public class ClashOfClansService : ClashOfClansBaseApiService
{
    public ClashOfClansService(HttpClient httpClient) : base(httpClient) { }
    internal async Task<ResponseClashOfClans<ClanResponse>> BuscarClan(string tag)
    {
        string uri = $"/v1/clans/{tag}";
        var request = CreateRequest<string>(null, HttpMethod.Get, uri);
        ResponseClashOfClans<ClanResponse> result = await SendRequest<ClanResponse>(request);
        return result;
    }
    public async Task<ResponseClashOfClans<WarResponse>> BuscarGuerra(string tag)
    {
        string uri = $"/v1/clans/{tag}/currentwar";
        var request = CreateRequest<string>(null, HttpMethod.Get, uri);
        var result = await SendRequest<WarResponse>(request);
        return result;
    }
    public async Task<ResponseClashOfClans<WarLogResponse>> BuscarWarLog(string tag)
    {
        string uri = $"/v1/clans/{tag}/warlog?limit=10";
        var request = CreateRequest<string>(null, HttpMethod.Get, uri);
        var result = await SendRequest<WarLogResponse>(request);
        return result;
    }
}
