using Quartz;
using System.Net.Http.Headers;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ClashOfClans.API.Data;
using ClashOfClans.API.Application.Commands.Clans;
using ClashOfClans.API.Core.CommandResults;
using MediatR;
using ClashOfClans.API.DTOs;
using ClashOfClans.API.Model.Clans;
using Microsoft.EntityFrameworkCore;

namespace ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans;

public class BuscarClanJob(ClashOfClansService clashOfClansService, ClashOfClansContext context, IMediator mediator) : IJob
{
    private readonly ClashOfClansService _clashOfClansService = clashOfClansService;
    private readonly ClashOfClansContext _context = context;

    private readonly IMediator _mediator = mediator;

    public async Task Execute(IJobExecutionContext context)
    {
        string tag = "#2L0UC9R8P";
        string encodedTag = Uri.EscapeDataString(tag);

        ClashClanResponse clanClashResponse = await _clashOfClansService.BuscarClan(encodedTag);
        if (clanClashResponse is null)
        {
            Console.WriteLine("Falha ao obter clan");
            return;
        }

        Clan? clan = await _context.Clans.FirstOrDefaultAsync(c => c.Tag == tag);

        if (clan is not null)
        {
            var request = new AtualizarClanRequest(clanClashResponse.Tag, clanClashResponse.Name, clanClashResponse.MemberList.Select(m => new MembroClanDTO() { Nome = m.Name, Tag = m.Tag }));

            CommandResult<AtualizarClanResponse> resultadoAtualizacao = await _mediator.Send(request);
            return;
        }

        Console.WriteLine($"{DateTime.Now} - Criando Clan");
        CriarClanRequest clanInputModel = new(clanClashResponse.Tag, clanClashResponse.Name, clanClashResponse.MemberList.Select(c => new MembroClanDTO() { Nome = c.Name, Tag = c.Tag }));
        CommandResult<CriarClanResponse> resultadoCriacao = await _mediator.Send(clanInputModel);
    }
}

public record ClashClanResponse
{
    public required string Tag { get; init; }
    public required string Name { get; init; }
    public List<ClanMemberList> MemberList { get; set; } = [];
}

public class ClanMemberList
{
    public required string Tag { get; init; }
    public required string Name { get; init; }
}
public class ClashOfClansService : ClashOfClansBaseApiService
{
    internal async Task<ClashClanResponse> BuscarClan(string tag)
    {
        string uri = $"/v1/clans/{tag}";
        var request = CreateRequest<string>(null, HttpMethod.Get, uri);
        var result = await SendRequest<ClashClanResponse>(request);
        return result.ResponseData!;
    }
    //public async Task<War> BuscarGuerra(string tag)
    //{
    //    string uri = $"/v1/clans/{tag}/currentwar";
    //    var request = CreateRequest<string>(null, HttpMethod.Get, uri);
    //    var result = await SendRequest<War>(request);
    //    return result.ResponseData!;
    //}


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

public class ClashOfClansBaseApiService
{
    private Uri _baseUri = new("https://api.clashofclans.com/v1/");
    private readonly HttpClient _httpClient;
    public ClashOfClansBaseApiService()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiIsImtpZCI6IjI4YTMxOGY3LTAwMDAtYTFlYi03ZmExLTJjNzQzM2M2Y2NhNSJ9.eyJpc3MiOiJzdXBlcmNlbGwiLCJhdWQiOiJzdXBlcmNlbGw6Z2FtZWFwaSIsImp0aSI6IjMzOTBmOGFhLWZhYWMtNGZhYi1hMWNlLWI3OWYyZjhiZjkzMCIsImlhdCI6MTc2NTgwMzAyMCwic3ViIjoiZGV2ZWxvcGVyL2YzM2YwYjI5LWM0NGItODk0Yi02MTY4LWI3YjNlNjUyNmI5YyIsInNjb3BlcyI6WyJjbGFzaCJdLCJsaW1pdHMiOlt7InRpZXIiOiJkZXZlbG9wZXIvc2lsdmVyIiwidHlwZSI6InRocm90dGxpbmcifSx7ImNpZHJzIjpbIjE4OS40OS43MS4yNDMiXSwidHlwZSI6ImNsaWVudCJ9XX0.GHuEW3TC5jvK2z91Cl1miRc1PmUN8wgPwugwmySkeya6Ciljwsl46ECsu8AKDa67yMFHTY58yepEo3jg2BBbtQ");
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
    protected async Task<ResponseClashOfClans<TResponse>> SendRequest<TResponse>(HttpRequestMessage request)
    {
        try
        {
            var response = await _httpClient.SendAsync(request);
            return await ParseReponse<TResponse>(response);
        }
        catch (Exception ex)
        {
            return new ResponseClashOfClans<TResponse>
            {
                IsValid = false,
                Erros = [$"Erro ao enviar requisição ao servidor {ex}"]
            };
        }
    }
    private static async Task<ResponseClashOfClans<T>> ParseReponse<T>(HttpResponseMessage response)
    {
        string contentString = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.Accepted || string.IsNullOrEmpty(contentString))
        {
            return new ResponseClashOfClans<T>
            {
                ResponseData = default,
                IsValid = true,
                Erros = Array.Empty<string>()
            };
        }

        if (!response.IsSuccessStatusCode)
        {
            return new ResponseClashOfClans<T>
            {
                ResponseData = default,
                IsValid = false,
                Erros = [contentString]
            };
        }

        try
        {
            var content = JsonConvert.DeserializeObject<T>(contentString, new JsonSerializerSettings
            {
                Error = (sender, errorArgs) =>
                {
                    var currentError = errorArgs.ErrorContext.Error.Message;
                    errorArgs.ErrorContext.Handled = true;
                }
            }); ;

            return new ResponseClashOfClans<T>
            {
                ResponseData = content,
                IsValid = true
            };
        }
        catch (Exception ex)
        {

            return new ResponseClashOfClans<T>
            {
                ResponseData = default,
                IsValid = false,
                Erros = [ex.Message]
            };
        }
    }
    protected HttpRequestMessage CreateRequest<TContent>(TContent content, HttpMethod method, string relativeUri)
    {
        StringContent? requestContent = default;

        if (content != null)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(content, settings);
            requestContent = new StringContent(json);
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        }

        var request = new HttpRequestMessage
        {
            Method = method,
            Content = requestContent,
            RequestUri = new Uri(_baseUri, relativeUri),
        };

        return request;
    }
}
public class ResponseClashOfClans<T>
{
    public bool IsValid { get; set; }
    public T? ResponseData { get; set; }
    public string[]? Erros { get; set; }
}

public class ClanWarLeagueGroup
{
    public string State { get; set; }
    public string Season { get; set; }
    public List<ClanWarLeagueClan>? Clans { get; set; }
    public List<ClanWarLeagueRound>? Rounds { get; set; }
}

public class ClanWarLeagueClan
{
    public string Tag { get; set; }
    public int ClanLevel { get; set; }
    public string Name { get; set; }
    public List<ClanWarLeagueClanMember> Members { get; set; }
}
public class ClanWarLeagueClanMember
{
    public string Tag { get; set; }
    public int TownHallLevel { get; set; }
    public string Name { get; set; }
}
public class ClanWarLeagueRound
{
    public List<string> WarTags { get; set; }
}

public class ClanWarLeague
{
    //public string State { get; set; }
    //[JsonConverter(typeof(CustomDateTimeConverter))]
    //public DateTime StartTime { get; set; }
    //[JsonConverter(typeof(CustomDateTimeConverter))]
    //public DateTime EndTime { get; set; }
    //public ClanWar Clan { get; set; }
    //public ClanWar Opponent { get; set; }
}
public class CriarClanInputModel
{
    public required string Tag { get; init; }
    public required string Nome { get; init; }
    public IEnumerable<MembroDTO> Membros { get; set; } = [];
}

public class MembroDTO
{
    public required string Tag { get; init; }
    public required string Nome { get; init; }
}
