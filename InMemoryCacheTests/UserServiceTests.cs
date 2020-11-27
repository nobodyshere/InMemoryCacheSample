using InMemoryCacheApi.Models;
using InMemoryCacheApi.Services;
using Xunit;

namespace InMemoryCacheTests
{
    public class UserServiceTests
    {
        [Fact]
        public void UserInsertSuccess()
        {
            // Arrange
            var user = new User
            {
                Id = 6,
                Age = 35,
                FirstName = "Ihor",
                SecondName = "Dyrman"
            };

            var userService = new UserService();

            // Act
            userService.InsertUser(user);

            // Assert
            var existUser = userService.GetUserById(user.Id);
            Assert.NotNull(existUser);
        }

        [Fact]
        public void UserInsertFailed()
        {
            // Arrange
            var user1 = new User
            {
                Id = 6,
                Age = 35,
                FirstName = "Ihor",
                SecondName = "Dyrman"
            };

            var user2 = new User
            {
                Id = 6,
                Age = 33,
                FirstName = "Ihor",
                SecondName = "Dyrman"
            };

            var userService = new UserService();

            // Act
            userService.InsertUser(user1);
            userService.InsertUser(user2);

            // Arrange
            var existUser = userService.GetUserById(6);
            Assert.NotNull(existUser);
            Assert.NotEqual(existUser.Age, user2.Age);
            Assert.Equal(existUser.FirstName, user2.FirstName);
        }

        [Fact]
        public void UserRemoveSuccess()
        {
            // Arrange
            var user = new User
            {
                Id = 6,
                Age = 35,
                FirstName = "Ihor",
                SecondName = "Dyrman"
            };

            var userService = new UserService();

            // Act
            userService.DeleteUser(6);

            // Assert
            Assert.Null(userService.GetUserById(6));
            Assert.NotNull(userService.GetUserById(5));
        }

        [Fact]
        public void UserUpdateSuccess()
        {
            // Arrange
            var user1 = new User
            {
                Id = 6,
                Age = 35,
                FirstName = "Ihor",
                SecondName = "Dyrman"
            };

            var user2 = new User
            {
                Id = 6,
                Age = 33,
                FirstName = "Not Ihor",
                SecondName = "Not Dyrman"
            };

            var userService = new UserService();
            userService.InsertUser(user1);

            // Act
            userService.UpdateUser(user2);
            var actualUser = userService.GetUserById(6);

            // Assert
            Assert.Equal(user2.Age, actualUser.Age);
            Assert.Equal(user2.FirstName, actualUser.FirstName);
            Assert.Equal(user2.SecondName, actualUser.SecondName);
        }

        [Fact]
        public void GetAllUsersNotNull()
        {
            // Arrange
            var userService = new UserService();

            // Act
            var users = userService.GetUsers();

            // Assert
            Assert.NotEmpty(users);
        }
    }
}