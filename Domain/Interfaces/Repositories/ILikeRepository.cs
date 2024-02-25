using Blog.Domain.Entities;

namespace Blog.Domain.Interfaces.Repositories;

public interface ILikeRepository
{
    Task<IEnumerable<object>> GetManyAsync(int? page = null, int? pageSize = null);
    Task CreateAsync(Like like);
    Task<bool> DeleteAsync(Like like);
}
