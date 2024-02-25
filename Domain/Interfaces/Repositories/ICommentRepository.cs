using Blog.Domain.Entities;

namespace Blog.Domain.Interfaces.Repositories;

public interface ICommentRepository
{
    Task<IEnumerable<object>> GetManyAsync(int? page = null, int? pageSize = null);
    Task<Comment?> GetOneAsync(Guid id);
    Task CreateAsync(Comment comment);
    Task<bool> UpdateAsync(Comment comment);
    Task<bool> DeleteAsync(Post post);
}
