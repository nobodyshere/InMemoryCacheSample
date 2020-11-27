using System;
using InMemoryCacheSample.Models;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCacheSample.Services
{
    public interface IMemoryCacheService
    {
        User GetCachedUser(int key);
        void SetCache(User user);
        void ClearCache(int id);
    }
}