using System.Collections.Generic;
using InMemoryCacheSample.Models;

namespace InMemoryCacheSample.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int id);
        void InsertUser(User user);
        void DeleteUser(int id);
        void UpdateUser(User user);
    }
}