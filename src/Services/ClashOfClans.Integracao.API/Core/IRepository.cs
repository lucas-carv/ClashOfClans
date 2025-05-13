using System.Data;

namespace ClashOfClans.Integracao.API.Core
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
