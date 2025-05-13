using ClashOfClans.Integracao.API.Core;
using ClashOfClans.Integracao.API.Data;
using ClashOfClans.Integracao.API.Model.Guerras;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClashOfClans.Integracao.API.Repositories
{
    public class GuerrasRepository : Repository<Guerra, ClashOfClansContext>, IGuerrasRepository
    {
        public GuerrasRepository(ClashOfClansContext context) : base(context)
        {
        }

        public IUnitOfWork UnitOfWork => _context;
        public IDbConnection Connection => _context.Database.GetDbConnection();

        public void Add(Guerra entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ObterGuerraPorData(DateTime dataGuerra)
        {
            throw new NotImplementedException();
        }

        public async Task<Guerra> ObterPorId(int id)
        {
            var result = await _context.Guerra
                .FirstOrDefaultAsync(f => f.Id == id);

            return result;
        }

        public IQueryable<Guerra> ObterTodos()
        {
            throw new NotImplementedException();
        }

        public void Update(Guerra entity)
        {
            throw new NotImplementedException();
        }
    }

    public interface IGuerrasRepository : IRepository<Guerra>
    {
        Task<bool> ObterGuerraPorData(DateTime dataGuerra);
    }
}
