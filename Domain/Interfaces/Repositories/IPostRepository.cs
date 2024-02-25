using Blog.Domain.Entities;

namespace Blog.Domain.Interfaces.Repositories;

public interface IPostRepository
{
    Task<IEnumerable<object>> GetManyAsync(int? page = null, int? pageSize = null);
    Task<Post?> GetOneAsync(Guid id);
    Task<int> CountCommentsAsync(Guid id);
    Task CreateAsync(Post post);
    Task<bool> UpdateAsync(Post post);
    Task<bool> DeleteAsync(Post post);
}
