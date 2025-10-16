using ClashOfClans.API.Model.Guerras;

namespace ClashOfClans.API.Repositories;

public interface IGuerraRepository : IRepository<Guerra>
{
    Task<Guerra> ObterGuerraPorDatas(DateTime inicioGuerra, DateTime fimGuerra);
}
