using System.Threading.Tasks;
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
        public async Task UserInsertSuccess()
        {
            // Arrange
            var context = new UsersDataFixture(nameof(UserInsertSuccess));
            var userService = new UserService(context.MemoryContext);

            // Act
            await userService.InsertUserAsync(_user1);

            // Assert
            var existUser = await userService.GetUserByIdAsync(_user1.Id);
            Assert.NotNull(existUser);

            context.Dispose();
        }

        [Fact]
        public async Task UserInsertFailed()
        {
            // Arrange
            var context = new UsersDataFixture(nameof(UserRemoveSuccess));
            var userService = new UserService(context.MemoryContext);

            // Act
            await userService.InsertUserAsync(_user1);
            await userService.InsertUserAsync(_user2);

            // Arrange
            var existUser = await userService.GetUserByIdAsync(6);
            Assert.NotNull(existUser);
            Assert.NotEqual(existUser.Age, _user2.Age);
            Assert.NotEqual(existUser.FirstName, _user2.FirstName);

            context.Dispose();
        }

        [Fact]
        public async Task UserRemoveSuccess()
        {
            // Arrange
            var context = new UsersDataFixture(nameof(UserRemoveSuccess));
            var userService = new UserService(context.MemoryContext);

            // Act
            await userService.DeleteUserAsync(6);

            // Assert
            Assert.Null(await userService.GetUserByIdAsync(6));
            Assert.NotNull(userService.GetUserByIdAsync(5));

            context.Dispose();
        }

        [Fact]
        public async Task UserUpdateSuccess()
        {
            // Arrange
            var context = new UsersDataFixture(nameof(UserUpdateSuccess));
            var userService = new UserService(context.MemoryContext);
            await userService.InsertUserAsync(_user1);

            // Act
            await userService.UpdateUserAsync(_user2);
            var actualUser = await userService.GetUserByIdAsync(6);

            // Assert
            Assert.Equal(_user2.Age, actualUser.Age);
            Assert.Equal(_user2.FirstName, actualUser.FirstName);
            Assert.Equal(_user2.SecondName, actualUser.SecondName);

            context.Dispose();
        }

        [Fact]
        public async Task GetAllUsersNotNull()
        {
            // Arrange
            var context = new UsersDataFixture(nameof(UserInsertSuccess));
            var userService = new UserService(context.MemoryContext);

            await userService.InsertUserAsync(_user1);

            // Act
            var users = await userService.GetUsersAsync();

            // Assert
            Assert.NotEmpty(users);

            context.Dispose();
        }
    }
}