using Blog.Api.Domain.Entities;

namespace Blog.Api.Domain.Interfaces.Repositories;

public interface ILikeRepository
{
    Task<IEnumerable<Like>> GetManyAsync(int? page = null, int? pageSize = null);
    Task CreateAsync(Like like);
    Task<bool> DeleteAsync(Like like);
}