using InMemoryCacheApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InMemoryCacheApi.Context
{
    public class InMemoryContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public InMemoryContext(DbContextOptions<InMemoryContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User {Id = 1, FirstName = "Innis", SecondName = "Mawford", Age = 95},
                new User {Id = 2, FirstName = "Gabrila", SecondName = "Drewell", Age = 35},
                new User {Id = 3, FirstName = "Gonzalo", SecondName = "Rentilll", Age = 75},
                new User {Id = 4, FirstName = "Daven", SecondName = "Streat", Age = 56},
                new User {Id = 5, FirstName = "Rianon", SecondName = "Gumey", Age = 73}
            );
        }
    }
}