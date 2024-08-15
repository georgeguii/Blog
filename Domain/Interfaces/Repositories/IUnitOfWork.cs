namespace Blog.Domain.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    Task CommitAsync(CancellationToken cancellationToken);
    void Rollback();
}