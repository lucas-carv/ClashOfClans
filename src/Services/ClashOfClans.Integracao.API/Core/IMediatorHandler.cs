namespace ClashOfClans.Integracao.API.Core
{
    public interface IMediatorHandler
    {
        //Task PublicarEvento<T>(T evento) where T : DomainEvent;
        Task<CommandResponse<TResponse>> EnviarComando<TResponse>(Command<CommandResponse<TResponse>> command);
    }
}
