using ClashOfClans.ETL.Models;

namespace ClashOfClans.ETL.Services;

public class ClashOfClansService : ClashOfClansBaseApiService
{
    internal async Task<Clan> BuscarClan(string tag)
    {
        string uri = $"/v1/clans/{tag}";
        var request = CreateRequest<string>(null, HttpMethod.Get, uri);
        var result = await SendRequest<Clan>(request);
        return result.ResponseData!;
    }
    public async Task<War> BuscarGuerra(string tag)
    {
        string uri = $"/v1/clans/{tag}/currentwar";
        var request = CreateRequest<string>(null, HttpMethod.Get, uri);
        var result = await SendRequest<War>(request);
        return result.ResponseData!;
    }
}
