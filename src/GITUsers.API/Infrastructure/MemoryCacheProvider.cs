using Microsoft.Extensions.Caching.Memory;
using System.Xml.Linq;

namespace GITUsers.API.Infrastructure
{
    public class MemoryCacheProvider : ICacheProvider
    {
        readonly IMemoryCache _cache;
        readonly ILogger<MemoryCacheProvider> _logger;

        public MemoryCacheProvider(IMemoryCache cache, ILogger<MemoryCacheProvider> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public T? GetFromCache<T>(string key) where T : class
        {
            try
            {
                T? value = null;
                _cache.TryGetValue(key, out value);
                return value;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving cache entry with key [{key}]!");
                return null;
            }
        }

        public void SetCache<T>(string key, T value, int seconds = 60) where T : class
        {
            try
            {
                _cache.Set(key, value, DateTimeOffset.Now.AddSeconds(seconds));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error saving cache entry with key [{key}]!");
            }
        }

        public void ClearCache(string key)
        {
            try
            {
                _cache.Remove(key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error removing cache entry with key [{key}]!");
            }
        }
    }
}
