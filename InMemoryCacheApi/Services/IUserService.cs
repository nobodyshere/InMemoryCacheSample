using System.Collections.Generic;
using System.Threading.Tasks;
using InMemoryCacheApi.Models;

namespace InMemoryCacheApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task InsertUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task UpdateUserAsync(User user);
    }
}