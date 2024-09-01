using Blog.Api.Domain.Interfaces.Repositories;
using Blog.Api.Infra.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace Blog.Api.Infra.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly BlogContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(BlogContext context)
    {
        _context = context;
    }

    public void BeginTransaction()
    {
        _transaction = _context.Database.BeginTransaction();
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
        _transaction?.Commit();
    }

    public void Rollback()
    {
        _transaction?.Rollback();
    }

    public void Dispose()
    {
        _transaction?.Dispose();
    }
}