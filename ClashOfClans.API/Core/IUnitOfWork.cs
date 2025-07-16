namespace ClashOfClans.API.Core;

public interface IUnitOfWork
{
    Task<bool> Commit();
}