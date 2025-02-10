using Microsoft.AspNetCore.Mvc;
using ReBottle.Models;
using ReBottle.Models.DTOs;
using ReBottle.Services.Interfaces;

namespace ReBottle.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            if (users == null) return NotFound();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserDTO userDTO)
        {
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Username = userDTO.UserName,
                Email = userDTO.Email,
                Phone = userDTO.Phone,
                Password = userDTO.Password
            };

            await _userService.AddUserAsync(user);

            return Ok("User created successfully");
        }

    }
}
