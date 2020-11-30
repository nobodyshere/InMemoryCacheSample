using System.Threading.Tasks;
using InMemoryCacheApi.Models;
using InMemoryCacheApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InMemoryCacheApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMemoryCacheService _cacheService;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IMemoryCacheService cacheService, IUserService userService)
        {
            _logger = logger;
            _cacheService = cacheService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            return Ok(await _userService.GetUsersAsync());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            var user = _cacheService.GetCachedUser(id);

            if (user == null)
            {
                user = await _userService.GetUserByIdAsync(id);
                _cacheService.SetCache(user);
            }

            if (user != null) return Ok(user);

            return BadRequest("User not found");
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAsync(User user)
        {
            if (user.FirstName == null && user.SecondName == null)
                return BadRequest("Wrong data format");

            if (await _userService.GetUserByIdAsync(user.Id) != null)
                return BadRequest("User already exist");

            await _userService.InsertUserAsync(user);

            if (_cacheService.GetCachedUser(user.Id) != null)
                _cacheService.ClearCache(user.Id);

            _cacheService.SetCache(user);
            return Ok();

        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync(User user)
        {
            var existUser = _cacheService.GetCachedUser(user.Id)
                ?? await _userService.GetUserByIdAsync(user.Id);

            if (existUser == null)
                return NotFound("User not found");

            await _userService.UpdateUserAsync(user);

            _cacheService.ClearCache(user.Id);
            _cacheService.SetCache(user);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveUserAsync(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
                return BadRequest("User not found");

            _cacheService.ClearCache(id);
            await _userService.DeleteUserAsync(id);

            return Ok();
        }
    }
}