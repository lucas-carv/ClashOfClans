using ClashOfClans.ETL.Models;
using ClashOfClans.ETL.Models.LigaDeClans;

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


    public async Task<ClanWarLeagueGroup> BuscarLiga(string clanTag)
    {
        string uri = $"/v1/clans/{clanTag}/currentwar/leaguegroup";
        var request = CreateRequest<string>(null, HttpMethod.Get, uri);
        var result = await SendRequest<ClanWarLeagueGroup>(request);
        return result.ResponseData!;
    }
}
