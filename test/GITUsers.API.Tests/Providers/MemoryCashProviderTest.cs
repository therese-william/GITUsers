using GITUsers.API.Infrastructure;
using GITUsers.API.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace GITUsers.API.Tests.Providers
{
    public class MemoryCashProviderTest
    {
        public MemoryCacheProvider GetCacheProvider()
        {
            var mockLogger = Mock.Of<ILogger<MemoryCacheProvider>>();
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();

            var memoryCache = serviceProvider.GetService<IMemoryCache>();
            return new MemoryCacheProvider(memoryCache, mockLogger);
        }

        [Fact]
        public void CacheProvider_CacheSuccess()
        {
            var provider = GetCacheProvider();
            provider.SetCache("test", "value", 60);
            var cachedValue = provider.GetFromCache<string>("test");
            Assert.Equal("value", cachedValue);
        }

        [Fact]
        public void CacheProvider_CacheTimeout()
        {
            var provider = GetCacheProvider();
            provider.SetCache("test", "value", 5);
            Thread.Sleep(6000);
            var cachedValue = provider.GetFromCache<string>("test");
            Assert.Null(cachedValue);
        }
    }
}
