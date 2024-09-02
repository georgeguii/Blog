namespace Blog.Api.Domain.Interfaces;

public interface ICachingService<T>
{
    Task<T?> GetAsync(string key, CancellationToken cancellationToken = default);
    Task SetAsync(string key, T value, CancellationToken cancellationToken = default);
}