namespace ClashOfClans.API.Core;

public interface IUnitOfWork
{
    Task Commit(CancellationToken cancellationToken);
}