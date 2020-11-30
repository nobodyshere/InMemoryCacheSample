using System;
using InMemoryCacheApi.Models;
using InMemoryCacheApi.Services;
using Microsoft.Extensions.Caching.Memory;
using Xunit;

namespace InMemoryCacheTests
{
    public class CacheTests
    {
        private readonly User _user1 = new User
        {
            Id = 1,
            FirstName = "Ihor",
            SecondName = "Dyrman",
            Age = 30
        };

        private readonly User _user2 = new User
        {
            Id = 2,
            FirstName = "Not Ihor",
            SecondName = "Not Dyrman",
            Age = 35
        };

        [Fact]
        public void AddedToCacheAfterSetCache()
        {
            // Arrange
            var memory = CreateMemoryCache();

            // Act
            memory.SetCache(_user1);
            memory.SetCache(_user2);
            var cachedUser1 = memory.GetCachedUser(_user1.Id);
            var cachedUser2 = memory.GetCachedUser(_user1.Id);

            // Assert
            Assert.NotNull(cachedUser1);
            Assert.NotNull(cachedUser2);
        }

        [Fact]
        public void RemovedAfterClearingCache()
        {
            // Arrange
            var memory = CreateMemoryCache();
            memory.SetCache(_user1);
            memory.SetCache(_user2);

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
            var memory = CreateMemoryCache();
            memory.SetCache(_user1);

            // Act
            var cachedUser1 = memory.GetCachedUser(1);
            var cachedUser2 = memory.GetCachedUser(2);
            var cachedUser3 = memory.GetCachedUser(65);

            // Assert
            Assert.NotNull(cachedUser1);
            Assert.Null(cachedUser2);
            Assert.Null(cachedUser3);
        }

        private static IMemoryCacheService CreateMemoryCache()
        {
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var memory = new MemoryCacheService(memoryCache);

            return memory;
        }
    }
}