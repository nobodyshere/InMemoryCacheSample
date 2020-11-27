using System;
using InMemoryCacheApi.Models;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCacheApi.Services
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly IMemoryCache _memoryCache;
        private const int CacheSize = 1024;
        private const int AbsoluteExpirationsMinutes = 10;
        private const int SlidingExpirationsMinutes = 5;

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public User GetCachedUser(int key)
        {
            _memoryCache.TryGetValue<User>(key, out User value);
            return value;
        }

        public void SetCache(User user)
        {
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(AbsoluteExpirationsMinutes),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromMinutes(SlidingExpirationsMinutes),
                Size = CacheSize,
            };

            _memoryCache.Set(user.Id, user, cacheExpiryOptions);
        }

        public void ClearCache(int id)
        {
            _memoryCache.Remove(id);
        }
    }
}