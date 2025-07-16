using ClashOfClans.API.Core;
using ClashOfClans.API.Data;
using ClashOfClans.API.Model;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClashOfClans.API.Repositories
{
    public class ClanRepository : IClanRepository
    {
        private readonly ClashOfClansContext _context;
        public ClanRepository(ClashOfClansContext clashOfClansContext)
        {
            _context = clashOfClansContext;
        }
        public IUnitOfWork UnitOfWork => _context;

        public void Add(Clan entity)
        {
            _context.Add(entity);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task<Clan> ObterPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Clan> ObterTodos()
        {
            throw new NotImplementedException();
        }

        public void Update(Clan entity)
        {
            throw new NotImplementedException();
        }
    }
}
