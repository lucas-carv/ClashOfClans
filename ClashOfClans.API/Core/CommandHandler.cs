using FluentValidation.Results;

namespace ClashOfClans.API.Core;

public class CommandHandler
{
    protected ValidationResult ValidationResult;

    public CommandHandler()
    {
        ValidationResult = new ValidationResult();
    }
    protected void AdicionarErro(string mensagem)
    {
        ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
    }

    protected async Task<ValidationResult> PersistirDados(IUnitOfWork unitOfWork)
    {
        if (!await unitOfWork.Commit()) AdicionarErro("Houve um erro ao persistir dados");
        return ValidationResult;
    }
}