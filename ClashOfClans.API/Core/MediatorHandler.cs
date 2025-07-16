using MediatR;

namespace ClashOfClans.API.Core;

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
}