using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReBottle.Models;
using ReBottle.Models.DTOs;
using ReBottle.Services.Interfaces;

namespace ReBottle.Web.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController(IUserService userService) : ControllerBase
    {
        public static User user = new();

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDTO request)
        {
            var user = await userService.AddUserAsync(request);
            if (user is null) return BadRequest("User allready exists");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDTO request)
        {
            var token = await userService.LoginAsync(request);
            if (token is null) return BadRequest("Invalid password or username");

            return Ok(token);
        }
    }
}
