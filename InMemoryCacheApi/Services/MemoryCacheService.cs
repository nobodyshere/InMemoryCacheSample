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
        private static object _sync;

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _sync = new object();
        }

        /// <summary>
        /// Get user from cache if exist
        /// </summary>
        /// <param name="key">User Id</param>
        /// <returns>User or null if user not found</returns>
        public User GetCachedUser(int key)
        {
            lock (_sync)
            {
                _memoryCache.TryGetValue<User>(key, out var value);
                return value;
            }
        }

        /// <summary>
        /// Save user to the memory cache
        /// </summary>
        /// <param name="user">User to save</param>
        public void SetCache(User user)
        {
            lock (_sync)
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
        }

        /// <summary>
        /// Remove user from cache
        /// </summary>
        /// <param name="id">User id</param>
        public void ClearCache(int id)
        {
            lock (_sync)
            {
                _memoryCache.Remove(id);
            }
        }
    }
}