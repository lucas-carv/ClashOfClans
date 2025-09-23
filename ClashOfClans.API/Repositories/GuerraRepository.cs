using ClashOfClans.API.Core;
using ClashOfClans.API.Data;
using ClashOfClans.API.Model.Guerras;
using Microsoft.EntityFrameworkCore;

namespace ClashOfClans.API.Repositories;

public class GuerraRepository : IGuerraRepository
{
    private readonly ClashOfClansContext _context;
    public GuerraRepository(ClashOfClansContext clashOfClansContext)
    {
        _context = clashOfClansContext;
    }
    public IUnitOfWork UnitOfWork => _context;

    public void Add(Guerra entity)
    {
        _context.Add(entity);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public Task<Guerra> ObterGuerraPorDatas(DateTime inicioGuerra, DateTime fimGuerra)
    {
        var guerra = _context.Guerras.FirstOrDefaultAsync(g => g.InicioGuerra == inicioGuerra && g.FimGuerra == fimGuerra);

        return guerra!;
    }

    public Task<Guerra> ObterPorId(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Guerra> ObterTodos()
    {
        throw new NotImplementedException();
    }

    public void Update(Guerra entity)
    {
        _context.Update(entity);
    }
}