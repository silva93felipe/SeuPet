using Microsoft.Extensions.Caching.Distributed;

namespace SeuPet.Infra.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _options;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
            _options = new DistributedCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromSeconds(60),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),
            };
        }

        public async Task<string> GetStringAsync(string key)
            => await _cache.GetStringAsync(key) ?? string.Empty;

        public async Task RemoveAsync(string key)
            => await _cache.RemoveAsync(key);

        public async Task SetStringAsync(string key, string objeto)
            => await _cache.SetStringAsync(key, objeto, _options);
    }
}