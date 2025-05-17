using MediatR;

namespace ClashOfClans.Integracao.API.Core
{
    public interface IMediatorHandler
    {
        //Task PublicarEvento<T>(T evento) where T : DomainEvent;
        Task<CommandResponse<TResponse>> EnviarComando<TResponse>(Command<CommandResponse<TResponse>> command);
    }
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CommandResponse<TResponse>> EnviarComando<TResponse>(Command<CommandResponse<TResponse>> command)
        {
            return await _mediator.Send(command);
        }

        //public async Task PublicarEvento<TEvent>(TEvent evento) where TEvent : DomainEvent
        //{
        //    if (_eventStore != null)
        //        await _eventStore.PersistirEvento<TEvent>(evento);

        //    await _mediator.Publish(evento);
        //}
    }
}
