namespace Blog.Infra.Caching;

public interface ICachingService
{
    Task SetAsync(string key, string value);
    Task<string> GetCAsync(string key);

}
