using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InMemoryCacheApi.Context;
using InMemoryCacheApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InMemoryCacheApi.Services
{
    public class UserService : IUserService
    {
        private readonly InMemoryContext _context;

        public UserService(InMemoryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get list of all users
        /// </summary>
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>User</returns>
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Add user to the list if it does not yet exist there
        /// </summary>
        /// <param name="user">User to add</param>
        public async Task InsertUserAsync(User user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

            if (existingUser == null)
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Remove user from the list
        /// </summary>
        /// <param name="id">Id of the user we need to remove</param>
        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Update exist user in the list
        /// </summary>
        /// <param name="user">User to update</param>
        public async Task UpdateUserAsync(User user)
        {
            var existUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

            if (existUser != null)
            {
                existUser.FirstName = user.FirstName;
                existUser.SecondName = user.SecondName;
                existUser.Age = user.Age;

                await _context.SaveChangesAsync();
            }
        }
    }
}