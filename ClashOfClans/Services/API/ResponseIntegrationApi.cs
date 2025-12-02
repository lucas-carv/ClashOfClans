namespace ClashOfClans.Services.API;

public class ResponseIntegrationApi<T>
{
    public bool IsValid { get; set; }
    public T ResponseData { get; set; }
    public string[] Erros { get; set; }
}
