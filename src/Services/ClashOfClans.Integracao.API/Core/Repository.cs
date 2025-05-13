using Microsoft.EntityFrameworkCore;

namespace ClashOfClans.Integracao.API.Core
{
    public class Repository<TEntity, TDbContext> : IRepository<TEntity>, IDisposable where TEntity : Entity, IAggregateRoot where TDbContext : DbContext, IUnitOfWork
    {
        protected internal readonly TDbContext _context;

        protected internal readonly DbSet<TEntity> _dbSet;

        public Repository(TDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public Task Add(TEntity entity)
        {
            _context.Add(entity);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task Update(TEntity entity)
        {
            _context.Update(entity);
            return Task.CompletedTask;
        }

        public Task Delete(TEntity entity)
        {
            _context.Remove(entity);
            return Task.CompletedTask;
        }

        public IQueryable<TEntity> ObterTodos()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> ObterPorId(int id)
        {
            throw new NotImplementedException();
        }

    }
}
