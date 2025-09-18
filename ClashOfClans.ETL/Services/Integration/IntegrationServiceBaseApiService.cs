using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using static System.Net.WebRequestMethods;

namespace ClashOfClans.ETL.Services.Integration;

public abstract class IntegrationServiceBaseApiService
{
    private readonly HttpClient _httpClient;
    protected readonly string _baseUrl = $"https://localhost:7016/api/v1";

    public IntegrationServiceBaseApiService()
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
            ResponseIntegrationApi<TResponse> response = new ResponseIntegrationApi<TResponse>();
            response.IsValid = false;
            response.Erros = new string[1] { $"Erro ao enviar requisição ao servidor {ex}" };
            return response;
        }
    }
    protected HttpRequestMessage CreateRequest<TContent>(TContent content, HttpMethod method, string relativeUri)
    {
        StringContent stringContent = null;
        if (content != null)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };
            var objetoSerializado = JsonConvert.SerializeObject(content, settings);

            stringContent = new StringContent(objetoSerializado, Encoding.UTF8, "application/json");
            //stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        }

        return new HttpRequestMessage
        {
            Method = method,
            Content = stringContent,
            RequestUri = new Uri(new Uri("https://localhost:7016"), relativeUri)
        };
    }
    private async Task<ResponseIntegrationApi<T>> ParseReponse<T>(HttpResponseMessage response)
    {
        string text = await response.Content.ReadAsStringAsync();
        if (response.StatusCode == HttpStatusCode.Accepted || string.IsNullOrEmpty(text))
        {
            return new ResponseIntegrationApi<T>
            {
                ResponseData = default(T),
                IsValid = true,
                Erros = Array.Empty<string>()
            };
        }

        if (!response.IsSuccessStatusCode)
        {
            ResponseIntegrationApi<T> responseIFood = new ResponseIntegrationApi<T>();
            responseIFood.ResponseData = default(T);
            responseIFood.IsValid = false;
            responseIFood.Erros = new string[1] { text };
            return responseIFood;
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
            });
            return new ResponseIntegrationApi<T>
            {
                ResponseData = responseData,
                IsValid = true
            };
        }
        catch (Exception ex)
        {
            ResponseIntegrationApi<T> responseIFood = new ResponseIntegrationApi<T>();
            responseIFood.ResponseData = default(T);
            responseIFood.IsValid = false;
            responseIFood.Erros = new string[1] { ex.Message };
            return responseIFood;
        }
    }

}
