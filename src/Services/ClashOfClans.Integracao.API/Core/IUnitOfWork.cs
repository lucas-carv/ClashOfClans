namespace ClashOfClans.Integracao.API.Core
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }}
