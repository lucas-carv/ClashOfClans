using FluentValidation.Results;
using MediatR;

namespace ClashOfClans.API.Core;

public record Command<TResponse> : Message, IRequest<TResponse>
{

    protected DateTime Timestamp { get; private set; }
    public ValidationResult ValidationResult { get; protected set; }

    protected Command()
    {
        Timestamp = DateTime.Now;
    }

    public virtual bool EhValido()
    {
        throw new NotImplementedException();
    }
    public ValidationResult ObterResultadoValidacao()
    {
        return ValidationResult;
    }

}
