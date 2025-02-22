using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ReBottle.Models;
using ReBottle.Models.DTOs;
using ReBottle.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace ReBottle.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
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
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        //[HttpPost]
        //public async Task<IActionResult> AddUser([FromBody] UserDTO userDTO)
        //{
        //    var user = new User
        //    {
        //        UserId = Guid.NewGuid(),
        //        Username = userDTO.Username,
        //        Email = userDTO.Email,
        //        Phone = userDTO.Phone,
        //        Password = userDTO.Password,
        //        Created = DateTime.Now,
        //        Updated = DateTime.Now,
        //        IsActive = true
        //    };

        //    await _userService.AddUserAsync(user);

        //    return Ok("User created successfully");
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            await _userService.DeleteUserAsync(id);
            return Ok("User deleted successfully");
        }

        [HttpGet("{id}/editable")]
        public async Task<IActionResult> GetEditableUser(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(new
            {
                user.Username,
                user.Email,
                user.Phone,
                user.IsActive
            });
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser(Guid id, [FromBody] JsonPatchDocument<UserUpdateDTO> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Patch document cannot be null.");
            }

            try
            {
                var updatedUser = await _userService.UpdateUserAsync(id, patchDoc);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                // Customize the error handling as needed.
                return NotFound(new { message = ex.Message });
            }
        }



    }
}
