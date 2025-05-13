using FluentValidation.Results;

namespace ClashOfClans.Integracao.API.Core
{
    public class CommandHandler
    {
        protected internal readonly IUnitOfWork _unitOfWork;
        protected ValidationResult ValidationResult;

        public CommandHandler(IUnitOfWork unitOfWork)
        {
            ValidationResult = new ValidationResult();
            _unitOfWork = unitOfWork;
        }
        protected void AdicionarErro(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }

        protected async Task<bool> PersistirDados()
        {
            return await _unitOfWork.Commit();
        }
    }
}
