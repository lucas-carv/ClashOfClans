namespace ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Responses;

public record ResponseClashOfClans<T>
{
    public bool IsValid { get; set; }
    public T? ResponseData { get; set; }
    public string[] Erros { get; set; } = [];
}