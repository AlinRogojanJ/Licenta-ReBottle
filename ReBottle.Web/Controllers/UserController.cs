using Microsoft.AspNetCore.Mvc;
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

    }
}
