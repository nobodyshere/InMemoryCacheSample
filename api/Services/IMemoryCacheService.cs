using System;
using InMemoryCacheApi.Models;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCacheApi.Services
{
    public interface IMemoryCacheService
    {
        User GetCachedUser(int key);
        void SetCache(User user);
        void ClearCache(int id);
    }
}