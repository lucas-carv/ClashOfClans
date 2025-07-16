﻿
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Http.Headers;

namespace ClashOfClans.Services;

public abstract class ClashOfClansBaseApiService
{
    private Uri _baseUri = new Uri("https://api.clashofclans.com/v1/");
    private readonly HttpClient _httpClient;
    public ClashOfClansBaseApiService()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
    protected async Task<ResponseClashOfClans<TResponse>> SendRequest<TResponse>(HttpRequestMessage request)
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiIsImtpZCI6IjI4YTMxOGY3LTAwMDAtYTFlYi03ZmExLTJjNzQzM2M2Y2NhNSJ9.eyJpc3MiOiJzdXBlcmNlbGwiLCJhdWQiOiJzdXBlcmNlbGw6Z2FtZWFwaSIsImp0aSI6IjAzYjJiYmY4LTk1MzYtNDRmMy04ZmQ5LTA0Njk1M2NkNTJjOSIsImlhdCI6MTc0NTQ0MDM4Nywic3ViIjoiZGV2ZWxvcGVyL2YzM2YwYjI5LWM0NGItODk0Yi02MTY4LWI3YjNlNjUyNmI5YyIsInNjb3BlcyI6WyJjbGFzaCJdLCJsaW1pdHMiOlt7InRpZXIiOiJkZXZlbG9wZXIvc2lsdmVyIiwidHlwZSI6InRocm90dGxpbmcifSx7ImNpZHJzIjpbIjE4OS44My4xOTkuNjQiXSwidHlwZSI6ImNsaWVudCJ9XX0.zkG5LA6HNpFyYK32SRzOHGmvgP-_RPEHdWRHQ2HSk_gjRi8l9s1TcM47XVZbxJFW3gaui0KIY9OnOCMg-SvJ7Q");
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
