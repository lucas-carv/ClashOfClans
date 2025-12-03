using ClashOfClans.Models;
using ClashOfClans.Services;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;

namespace ClashOfClans.Api;

public class ClashOfClansApiService 
{
    private readonly HttpClient _httpClient;
    public ClashOfClansApiService()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<ResponseClashOfClans<TResponse>> Send<TResponse, TContent>(TContent content, HttpMethod method, string uri)
    {
        HttpRequestMessage request = CreateRequest(content, method, uri);

        return await SendRequest<TResponse>(request);
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
                Erros = new string[] { $"Erro ao enviar requisição ao servidor {ex}" }
            };
        }
    }
    private async Task<ResponseClashOfClans<T>> ParseReponse<T>(HttpResponseMessage response)
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
                Erros = new string[] { contentString }
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
                Erros = new string[] { ex.Message }
            };
        }
    }
    protected HttpRequestMessage CreateRequest<TContent>(TContent content, HttpMethod method, string relativeUri)
    {
        StringContent requestContent = default;

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

        Uri _baseUri = new Uri("https://merchant-api.ifood.com.br/");

        var request = new HttpRequestMessage
        {
            Method = method,
            Content = requestContent,
            RequestUri = new Uri(_baseUri, relativeUri),
        };

        return request;
    }
}