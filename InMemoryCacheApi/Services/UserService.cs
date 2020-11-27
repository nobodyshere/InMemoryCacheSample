using System;
using System.Collections.Generic;
using System.Linq;
using InMemoryCacheApi.Models;

namespace InMemoryCacheApi.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new List<User>();

        public UserService()
        {
            _users.AddRange(new List<User>
            {
                new User
                {
                    Id = 1,
                    FirstName = "Innis",
                    SecondName = "Mawford",
                    Age = 95
                },
                new User
                {
                    Id = 2,
                    FirstName = "Gabrila",
                    SecondName = "Drewell",
                    Age = 35
                },
                new User
                {
                    Id = 3,
                    FirstName = "Gonzalo",
                    SecondName = "Rentilll",
                    Age = 75
                },
                new User
                {
                    Id = 4,
                    FirstName = "Daven",
                    SecondName = "Streat",
                    Age = 56
                },
                new User
                {
                    Id = 5,
                    FirstName = "Rianon",
                    SecondName = "Gumey",
                    Age = 73
                }
            });
        }

        public IEnumerable<User> GetUsers()
        {
            return _users;
        }

        public User GetUserById(int id)
        {
            var user = _users.FirstOrDefault(x => x.Id == id);
            return user;
        }

        public void InsertUser(User user)
        {
            var existingUser = _users.FirstOrDefault(x => x.Id == user.Id);

            if (existingUser == null) _users.Add(user);
        }

        public void DeleteUser(int id)
        {
            var user = _users.FirstOrDefault(x => x.Id == id);

            if (user != null) _users.Remove(user);
        }

        public void UpdateUser(User user)
        {
            var existUser = _users.FirstOrDefault(x => x.Id == user.Id);

            if (existUser != null)
            {
                existUser.FirstName = user.FirstName;
                existUser.SecondName = user.SecondName;
                existUser.Age = user.Age;
            }
        }
    }
}