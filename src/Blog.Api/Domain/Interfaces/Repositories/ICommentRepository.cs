using Blog.Api.Domain.Entities;

namespace Blog.Api.Domain.Interfaces.Repositories;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> GetManyAsync(int? page = null, int? pageSize = null);
    Task<Comment?> GetOneAsync(Guid id);
    Task CreateAsync(Comment comment);
    Task<bool> UpdateAsync(Comment comment);
    Task<bool> DeleteAsync(Post post);
}