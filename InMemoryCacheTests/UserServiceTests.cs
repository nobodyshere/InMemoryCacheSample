using InMemoryCacheApi.Models;
using InMemoryCacheApi.Services;
using Xunit;

namespace InMemoryCacheTests
{
    public class UserServiceTests
    {
        private readonly User _user1 = new User
        {
            Id = 6,
            Age = 35,
            FirstName = "Ihor",
            SecondName = "Dyrman"
        };

        private readonly User _user2 = new User
        {
            Id = 6,
            Age = 33,
            FirstName = "Not Ihor",
            SecondName = "Not Dyrman"
        };

        [Fact]
        public void UserInsertSuccess()
        {
            // Arrange
            var userService = new UserService();

            // Act
            userService.InsertUser(_user1);

            // Assert
            var existUser = userService.GetUserById(_user1.Id);
            Assert.NotNull(existUser);
        }

        [Fact]
        public void UserInsertFailed()
        {
            // Arrange
            var userService = new UserService();

            // Act
            userService.InsertUser(_user1);
            userService.InsertUser(_user2);

            // Arrange
            var existUser = userService.GetUserById(6);
            Assert.NotNull(existUser);
            Assert.NotEqual(existUser.Age, _user2.Age);
            Assert.NotEqual(existUser.FirstName, _user2.FirstName);
        }

        [Fact]
        public void UserRemoveSuccess()
        {
            // Arrange
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
            var userService = new UserService();
            userService.InsertUser(_user1);

            // Act
            userService.UpdateUser(_user2);
            var actualUser = userService.GetUserById(6);

            // Assert
            Assert.Equal(_user2.Age, actualUser.Age);
            Assert.Equal(_user2.FirstName, actualUser.FirstName);
            Assert.Equal(_user2.SecondName, actualUser.SecondName);
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