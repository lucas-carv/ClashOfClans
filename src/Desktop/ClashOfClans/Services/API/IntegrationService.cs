using ClashOfClans.Models;

namespace ClashOfClans.Services.API;

public class IntegrationService : IntegrationServiceBaseApi
{
    public async Task<List<MembroViewModel>> ObterMembros(string clanTag)
    {
        string encodedTag = Uri.EscapeDataString(clanTag);

        string uri = $"{_baseUrl}/membro/clanTag/{encodedTag}";
        var result = await Send<List<MembroViewModel>>(HttpMethod.Get, uri);
        return result.ResponseData;
    }
}
