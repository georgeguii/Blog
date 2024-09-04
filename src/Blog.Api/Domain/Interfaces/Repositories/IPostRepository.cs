using Blog.Api.Domain.Entities;

namespace Blog.Api.Domain.Interfaces.Repositories;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetManyAsync(int? page = null, int? pageSize = null);
    Task<Post?> GetOneAsync(Guid id);
    Task<int> CountCommentsAsync(Guid id);
    Task CreateAsync(Post post, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Post post);
    Task<bool> DeleteAsync(Post post);
}