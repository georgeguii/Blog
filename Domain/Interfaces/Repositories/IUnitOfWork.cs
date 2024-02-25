namespace Blog.Domain.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    Task Commit(CancellationToken cancellationToken);
    void Rollback();
}