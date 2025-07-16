using ClashOfClans.API.Core;
using System.Data;

namespace ClashOfClans.API.Repositories;

public interface IRepository<T> : IDisposable where T : IAggregateRoot
{
    IQueryable<T> ObterTodos();
    Task<T> ObterPorId(int id);
    void Add(T entity);
    void Update(T entity);
    IUnitOfWork UnitOfWork { get; }
}
