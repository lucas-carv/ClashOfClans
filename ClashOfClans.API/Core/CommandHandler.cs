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
        var result = await unitOfWork.Commit();

        if (!result) AdicionarErro("Houve um erro ao persistir dados");
        return ValidationResult;
    }
}