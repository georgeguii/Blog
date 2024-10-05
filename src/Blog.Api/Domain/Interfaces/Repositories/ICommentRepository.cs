using Blog.Api.Domain.Entities;

namespace Blog.Api.Domain.Interfaces.Repositories;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> GetManyAsync(int? page = null, int? pageSize = null);
    Task<Comment?> GetOneAsync(Guid commentId, Guid userId);
    Task CreateAsync(Comment comment, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Comment comment);
    Task<bool> DeleteAsync(Comment post);
}