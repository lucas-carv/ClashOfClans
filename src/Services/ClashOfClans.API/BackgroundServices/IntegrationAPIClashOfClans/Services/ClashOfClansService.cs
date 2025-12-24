using ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Responses;

namespace ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Services;
public class ClashOfClansService : ClashOfClansBaseApiService
{
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


    //public async Task<ClanWarLeagueGroup> BuscarGrupoLiga(string clanTag)
    //{
    //    string uri = $"/v1/clans/{clanTag}/currentwar/leaguegroup";
    //    var request = CreateRequest<string>(null, HttpMethod.Get, uri);
    //    var result = await SendRequest<ClanWarLeagueGroup>(request);
    //    return result.ResponseData!;
    //}

    //public async Task<ClanWarLeague> BuscarGuerraDaLiga(string guerraTag)
    //{
    //    string uri = $"/v1/clanwarleagues/wars/{guerraTag}";
    //    var request = CreateRequest<string>(null, HttpMethod.Get, uri);
    //    var result = await SendRequest<ClanWarLeague>(request);
    //    return result.ResponseData!;
    //}
}
