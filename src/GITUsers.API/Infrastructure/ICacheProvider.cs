namespace GITUsers.API.Infrastructure
{
    public interface ICacheProvider
    {
        T? GetFromCache<T>(string key) where T : class;
        void SetCache<T>(string key, T value, int seconds = 60) where T : class;
        void ClearCache(string key);
    }
}
