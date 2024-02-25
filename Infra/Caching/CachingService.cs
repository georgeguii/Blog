namespace Blog.Infra.Caching;

public class CachingService : ICachingService
{
    public Task<string> GetCAsync(string key)
    {
        throw new NotImplementedException();
    }

    public Task SetAsync(string key, string value)
    {
        throw new NotImplementedException();
    }
}
