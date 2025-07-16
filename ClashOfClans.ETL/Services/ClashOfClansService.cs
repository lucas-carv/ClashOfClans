using ClashOfClans.ETL.Models;

namespace ClashOfClans.ETL.Services;

public class ClashOfClansService : ClashOfClansBaseApiService
{
    internal async Task<Clan?> BuscarClan(string tag)
    {
        string encodedTag = Uri.EscapeDataString(tag);
        string uri = $"/v1/clans/{encodedTag}";
        var request = CreateRequest<string>(null, HttpMethod.Get, uri);
        var result = await SendRequest<Clan>(request);
        return result.ResponseData;
    }
}
