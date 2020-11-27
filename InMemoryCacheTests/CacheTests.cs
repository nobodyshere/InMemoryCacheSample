using System;
using InMemoryCacheApi.Models;
using InMemoryCacheApi.Services;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;

namespace InMemoryCacheTests
{
    public class CacheTests
    {
        [Fact]
        public void AddedToCacheAfterSetCache()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                FirstName = "Ihor",
                SecondName = "Dyrman",
                Age = 30
            };

            var user2 = new User
            {
                Id = 2,
                FirstName = "Ihor",
                SecondName = "Dyrman",
                Age = 35
            };

            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var memory = new MemoryCacheService(memoryCache);

            // Act
            memory.SetCache(user);
            memory.SetCache(user2);
            var cachedUser1 = memory.GetCachedUser(user.Id);
            var cachedUser2 = memory.GetCachedUser(user.Id);

            // Assert
            Assert.NotNull(cachedUser1);
            Assert.NotNull(cachedUser2);
        }

        [Fact]
        public void RemovedAfterClearingCache()
        {
            // Arrange
            var user1 = new User
            {
                Id = 1,
                FirstName = "Ihor",
                SecondName = "Dyrman",
                Age = 30
            };

            var user2 = new User
            {
                Id = 2,
                FirstName = "Ihor",
                SecondName = "Dyrman",
                Age = 30
            };

            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var memory = new MemoryCacheService(memoryCache);
            memory.SetCache(user1);
            memory.SetCache(user2);

            // Act
            memory.ClearCache(1);
            var cachedUser1 = memory.GetCachedUser(1);
            var cachedUser2 = memory.GetCachedUser(2);

            // Assert
            Assert.Null(cachedUser1);
            Assert.NotNull(cachedUser2);
        }

        [Fact]
        public void GetCachedValue()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                FirstName = "Ihor",
                SecondName = "Dyrman",
                Age = 30
            };

            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var memory = new MemoryCacheService(memoryCache);
            memory.SetCache(user);

            // Act
            var cachedUser1 = memory.GetCachedUser(1);
            var cachedUser2 = memory.GetCachedUser(2);
            var cachedUser3 = memory.GetCachedUser(65);

            // Assert
            Assert.NotNull(cachedUser1);
            Assert.Null(cachedUser2);
            Assert.Null(cachedUser3);
        }
    }
}