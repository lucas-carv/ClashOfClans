using ClashOfClans.Models;

namespace ClashOfClans.Services;

public class ClanService : ClashOfClansBaseApiService, IClanService
{
    public async Task<Clan> BuscarClan(string tag)
    {
        string encodedTag = Uri.EscapeDataString(tag);
        string uri = $"/v1/clans/{encodedTag}";
        var request = CreateRequest<string>(null, HttpMethod.Get, uri);
        var result = await SendRequest<Clan>(request);
        return result.ResponseData;
    }

    public async Task<List<MembroViewModel>> BuscarMembros(string tag)
    {
        string encodedTag = Uri.EscapeDataString(tag);
        string uri = $"/v1/clans/{encodedTag}/members";
        var request = CreateRequest<string>(null, HttpMethod.Get, uri);
        var result = await SendRequest<List<MembroViewModel>>(request);
        return result.ResponseData;
    }
}

