using System;
using InMemoryCacheApi.Context;
using InMemoryCacheApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InMemoryCacheTests
{
    public class UsersDataFixture : IDisposable
    {
        public UsersDataFixture(string name)
        {
            var options = new DbContextOptionsBuilder<InMemoryContext>()
                .UseInMemoryDatabase(name)
                .Options;

            MemoryContext = new InMemoryContext(options);
        }

        public InMemoryContext MemoryContext { get; }

        public void Dispose()
        {
            MemoryContext?.Database.EnsureDeleted();
            MemoryContext?.Dispose();
        }
    }
}