using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace ClashOfClans.ETL.Services.Integration;

public abstract class IntegrationServiceBaseApi
{
    private readonly HttpClient _httpClient;
    //protected readonly string _baseUrl = $"https://localhost:7016/api/v1";
    protected readonly string _baseUrl = $"https://clashofclans-1-bwjm.onrender.com/api/v1";

    public IntegrationServiceBaseApi()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
    public async Task<ResponseIntegrationApi<TResponse>> Send<TResponse>(HttpMethod method, string uri)
    {
        HttpRequestMessage request = CreateRequest<string>(null, method, uri);
        return await SendRequest<TResponse>(request);
    }

    public async Task<ResponseIntegrationApi<TResponse>> Send<TResponse, TContent>(TContent content, HttpMethod method, string uri)
    {
        HttpRequestMessage request = CreateRequest(content, method, uri);
        return await SendRequest<TResponse>(request);
    }
    protected async Task<ResponseIntegrationApi<TResponse>> SendRequest<TResponse>(HttpRequestMessage request)
    {
        try
        {
            var result = await _httpClient.SendAsync(request);
            return await ParseReponse<TResponse>(result);
        }
        catch (Exception ex)
        {
            ResponseIntegrationApi<TResponse> response = new()
            {
                IsValid = false,
                Erros = [$"Erro ao enviar requisição ao servidor {ex}"]
            };
            return response;
        }
    }
    protected static HttpRequestMessage CreateRequest<TContent>(TContent? content, HttpMethod method, string relativeUri)
    {
        StringContent? stringContent = null;
        if (content != null)
        {
            JsonSerializerSettings settings = new()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };
            var objetoSerializado = JsonConvert.SerializeObject(content, settings);

            stringContent = new StringContent(objetoSerializado, Encoding.UTF8, "application/json");
        }

        return new HttpRequestMessage
        {
            Method = method,
            Content = stringContent,
            RequestUri = new Uri(new Uri("https://clashofclans-1-bwjm.onrender.com"), relativeUri)
        };
    }
    private static async Task<ResponseIntegrationApi<T>> ParseReponse<T>(HttpResponseMessage response)
    {
        string text = await response.Content.ReadAsStringAsync();
        if (response.StatusCode == HttpStatusCode.Accepted || string.IsNullOrEmpty(text))
        {
            return new ResponseIntegrationApi<T>
            {
                ResponseData = default,
                IsValid = true,
                Erros = Array.Empty<string>()
            };
        }

        if (!response.IsSuccessStatusCode)
        {
            ResponseIntegrationApi<T> responseIntegration = new()
            {
                ResponseData = default,
                IsValid = false,
                Erros = [text]
            };
            return responseIntegration;
        }

        try
        {
            T responseData = JsonConvert.DeserializeObject<T>(text, new JsonSerializerSettings
            {
                Error = delegate (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs errorArgs)
                {
                    _ = errorArgs.ErrorContext.Error.Message;
                    errorArgs.ErrorContext.Handled = true;
                }
                !
            })!;
            return new ResponseIntegrationApi<T>
            {
                ResponseData = responseData,
                IsValid = true
            };
        }
        catch (Exception ex)
        {
            ResponseIntegrationApi<T> responseIntegracao = new()
            {
                ResponseData = default,
                IsValid = false,
                Erros = [ex.Message]
            };
            return responseIntegracao;
        }
    }
}
