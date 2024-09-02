using System.Text.Json;
using Blog.Api.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Blog.Api.Infra.Caching;

public class CachingService<T>(IDistributedCache cache, TimeSpan ttl, TimeSpan idleTime) : ICachingService<T>
{
    private readonly DistributedCacheEntryOptions _options = new()
    {
        AbsoluteExpirationRelativeToNow = ttl,
        SlidingExpiration = idleTime
    };

    public async Task<T?> GetAsync(string key, CancellationToken cancellationToken = default)
    {
        var cachedData = await cache.GetStringAsync(key, cancellationToken);

        if (string.IsNullOrEmpty(cachedData))
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(cachedData);
    }

    public async Task SetAsync(string key, T value, CancellationToken cancellationToken = default)
    {
        var valueSerialized = JsonSerializer.Serialize(value);
        await cache.SetStringAsync(key, valueSerialized, _options, cancellationToken);
    }
}