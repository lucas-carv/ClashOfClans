using FluentValidation.Results;

namespace ClashOfClans.API.Core;

public class CommandResponse<TResponse>
{
    public ValidationResult ValidationResult { get; private set; }
    public TResponse Response { get; private set; }
    public CommandResponse(ValidationResult validationResult, TResponse response = default)
    {
        ValidationResult = validationResult;
        Response = response;
    }
    public CommandResponse(TResponse response)
    {
        ValidationResult = new();
        Response = response;
    }
}
