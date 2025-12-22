using ClashOfClans.ETL.Models;
using ClashOfClans.ETL.Models.LeagueClans;
using ClashOfClans.ETL.Models.Wars;

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
    public async Task<WarResponse> BuscarGuerra(string tag)
    {
        string uri = $"/v1/clans/{tag}/currentwar";
        var request = CreateRequest<string>(null, HttpMethod.Get, uri);
        var result = await SendRequest<WarResponse>(request);
        return result.ResponseData!;
    }


    public async Task<ClanWarLeagueGroupResponse> BuscarGrupoLiga(string clanTag)
    {
        string uri = $"/v1/clans/{clanTag}/currentwar/leaguegroup";
        var request = CreateRequest<string>(null, HttpMethod.Get, uri);
        var result = await SendRequest<ClanWarLeagueGroupResponse>(request);
        return result.ResponseData!;
    }

    public async Task<ClanWarLeagueDTO> BuscarGuerraDaLiga(string guerraTag)
    {
        string uri = $"/v1/clanwarleagues/wars/{guerraTag}";
        var request = CreateRequest<string>(null, HttpMethod.Get, uri);
        var result = await SendRequest<ClanWarLeagueDTO>(request);
        return result.ResponseData!;
    }
}
