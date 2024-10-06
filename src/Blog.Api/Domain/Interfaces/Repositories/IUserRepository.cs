using Blog.Api.Domain.Entities;

namespace Blog.Api.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<bool> CheckIfEmailIsAlreadyRegistered(string email);
    Task<bool> CheckIfDocumentIsAlreadyRegistered(string document);
    Task<bool> CheckIfNicknameIsAlreadyRegistered(string nickname);
    
    Task<IEnumerable<User>> GetManyAsync(string? email = null, string? nickname = null, string? name = null,
        int? page = null, int? pageSize = null);

    Task<User?> GetOneAsync(Guid id);
    Task<User?> GetOneAsync(string nickname);
    Task CreateAsync(User user);
    Task<bool> UpdateAsync(User user);
    Task<bool> DisableAsync(User user);
}