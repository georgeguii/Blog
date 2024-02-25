using Blog.Domain.Entities;

namespace Blog.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<object>> GetManyAsync(string? email = null, string? nickname = null, string? name = null, int? page = null, int? pageSize = null);
    Task<User?> GetOneAsync(Guid id);
    Task CreateAsync(User user);
    Task<bool> UpdateAsync(User user);
    Task<bool> DisableAsync(User user);
}
