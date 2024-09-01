using Blog.Api.Domain.Entities;

namespace Blog.Api.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetManyAsync(string? email = null, string? nickname = null, string? name = null,
        int? page = null, int? pageSize = null);

    Task<User?> GetOneAsync(Guid id);
    Task CreateAsync(User user);
    Task<bool> UpdateAsync(User user);
    Task<bool> DisableAsync(User user);
}