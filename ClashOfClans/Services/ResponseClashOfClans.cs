namespace ClashOfClans.Services;

public class ResponseClashOfClans<T>
{
    public bool IsValid { get; set; }
    public T? ResponseData { get; set; }
    public string[] Erros { get; set; } = [];
}