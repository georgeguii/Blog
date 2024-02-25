using Blog.Domain.Interfaces.Repositories;
using Blog.Infra.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace Blog.Infra.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    private readonly BlogContext _context;

    public UnitOfWork(BlogContext ibgeContext)
    {
        _context = ibgeContext;
    }

    public void BeginTransaction()
    {
        _transaction = _context.Database.BeginTransaction();
    }

    public async Task Commit(CancellationToken cancellationToken)
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