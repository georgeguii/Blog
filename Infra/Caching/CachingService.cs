using Microsoft.Extensions.Caching.Distributed;

namespace Blog.Infra.Caching;

public class CachingService : ICachingService
{
    private readonly IDistributedCache _cache;

    private readonly DistributedCacheEntryOptions _options;

    public CachingService(IDistributedCache cache)
    {
        _cache = cache;
        _options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
            SlidingExpiration = TimeSpan.FromSeconds(1200),
        };
    }

    public async Task<string?> GetAsync(string key, CancellationToken cancellationToken = default)
        => await _cache.GetStringAsync(key, cancellationToken);

    public async Task SetAsync(string key, string value)
    {
        await _cache.SetStringAsync(key, value);
    }
}
