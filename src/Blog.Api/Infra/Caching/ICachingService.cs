namespace Blog.Api.Infra.Caching;

public interface ICachingService
{
    Task SetAsync(string key, string value);
    Task<string?> GetAsync(string key, CancellationToken cancellationToken);
}