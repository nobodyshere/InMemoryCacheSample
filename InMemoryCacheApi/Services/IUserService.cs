using System.Collections.Generic;
using InMemoryCacheApi.Models;

namespace InMemoryCacheApi.Services
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