using ClashOfClans.ETL.Models;
using System.Net.Http.Headers;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ClashOfClans.ETL.Services;

public class ClashOfClansBaseApiService
{
    private Uri _baseUri = new("https://api.clashofclans.com/v1/");
    private readonly HttpClient _httpClient;
    public ClashOfClansBaseApiService()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiIsImtpZCI6IjI4YTMxOGY3LTAwMDAtYTFlYi03ZmExLTJjNzQzM2M2Y2NhNSJ9.eyJpc3MiOiJzdXBlcmNlbGwiLCJhdWQiOiJzdXBlcmNlbGw6Z2FtZWFwaSIsImp0aSI6ImU0MzkwYWY3LTAzYzAtNDFkNS1hNWFhLTU0NjQ3ZTYxYTQyNCIsImlhdCI6MTc1ODE1Mjg5Mywic3ViIjoiZGV2ZWxvcGVyL2YzM2YwYjI5LWM0NGItODk0Yi02MTY4LWI3YjNlNjUyNmI5YyIsInNjb3BlcyI6WyJjbGFzaCJdLCJsaW1pdHMiOlt7InRpZXIiOiJkZXZlbG9wZXIvc2lsdmVyIiwidHlwZSI6InRocm90dGxpbmcifSx7ImNpZHJzIjpbIjE4OS43MC4xMTkuMjM2Il0sInR5cGUiOiJjbGllbnQifV19.h35T55ZKG95mL4xCs1iQaGoPYkgiqpGruHcUHy_Edra2-LS56NDLsmnAl_JteWt0VKo1mtLQBCPg4duGhr6jRg");
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