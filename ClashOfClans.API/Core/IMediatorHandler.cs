namespace ClashOfClans.API.Core;

public interface IMediatorHandler
{
    //Task PublicarEvento<T>(T evento) where T : DomainEvent;
    //Task<ValidationResult> EnviarComando<T>(T comando) where T : Command;
    Task<CommandResponse<TResponse>> EnviarComando<TResponse>(Command<CommandResponse<TResponse>> command);
}
