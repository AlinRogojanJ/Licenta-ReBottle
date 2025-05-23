using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ReBottle.Models;
using ReBottle.Models.DTOs;
using ReBottle.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ReBottle.Models.Data;

namespace ReBottle.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ReBottleContext _reBottleContext;
        public UserController(IUserService userService, ReBottleContext reBottleContext)
        {
            _userService = userService;
            _reBottleContext = reBottleContext;
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

        //[HttpGet("{id}/editable")]
        //public async Task<IActionResult> GetEditableUser(Guid id)
        //{
        //    var user = await _userService.GetUserByIdAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(new
        //    {
        //        user.Username,
        //        user.Email,
        //        user.Phone,
        //        user.IsActive
        //    });
        //}

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser(Guid id, [FromBody] JsonPatchDocument<UserDTO> patchDoc)
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
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("update-avatar/{id}")]
        public async Task<IActionResult> UpdateAvatar([FromBody] UserAvatarDTO dto, [FromRoute] Guid id)
        {
            var user = await _reBottleContext.Users.FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
                return NotFound();

            user.Avatar = dto.Avatar;
            await _reBottleContext.SaveChangesAsync();

            return Ok(new { avatar = user.Avatar });
        }

    }
}
