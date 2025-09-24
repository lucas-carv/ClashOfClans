using ClashOfClans.API.Model.Guerras;
using CSharpFunctionalExtensions;

namespace ClashOfClans.API.Repositories;

public interface IGuerraRepository : IRepository<Guerra>
{
    Task<Maybe<Guerra>> ObterGuerraPorDatas(DateTime inicioGuerra, DateTime fimGuerra);
}
