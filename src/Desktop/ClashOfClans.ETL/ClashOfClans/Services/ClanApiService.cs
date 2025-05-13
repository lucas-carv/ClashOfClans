using ClashOfClans.ETL.ClashOfClans.Model;
using System.Net.Http;

namespace ClashOfClans.ETL.ClashOfClans.Services;

public class ClanApiService : ClashOfClansBaseApiService, IClanApiService
{
    public async Task<Guerras> ObterGuerras()
    {
        string clanTag = "#2LOUC9R8P";
        string encodedTag = Uri.EscapeDataString(clanTag); // resulta em "%232LOUC9R8P"
        string uri = $"/v1/clans/{encodedTag}/warlog";
        var request = CreateRequest<string>(null, HttpMethod.Get, uri);
        var result = await SendRequest<Guerras>(request);
        return result.ResponseData;
    }
}
