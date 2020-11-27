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
        public IActionResult Users()
        {
            return Ok(_userService.GetUsers());
        }


        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _cacheService.GetCachedUser(id);

            if (user == null)
            {
                user = _userService.GetUserById(id);
                _cacheService.SetCache(user);
            }

            if (user != null) return Ok(user);
            return BadRequest("User not found");
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            if (user.FirstName != null
                || user.SecondName != null)
            {
                if (_userService.GetUserById(user.Id) != null)
                    return BadRequest("User already exist");

                _userService.InsertUser(user);

                if (_cacheService.GetCachedUser(user.Id) != null)
                    _cacheService.ClearCache(user.Id);

                _cacheService.SetCache(user);
                return Ok();
            }

            return BadRequest("Wrong data format");
        }

        [HttpPut]
        public IActionResult UpdateUser(User user)
        {
            var existUser = _cacheService.GetCachedUser(user.Id)
                ?? _userService.GetUserById(user.Id);

            if (existUser == null)
                return NotFound("User not found");

            _userService.UpdateUser(user);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveUser(int id)
        {
            var user = _userService.GetUserById(id);

            if (user == null)
                return BadRequest("User not found");

            _cacheService.ClearCache(id);
            _userService.DeleteUser(id);

            return Ok();
        }
    }
}