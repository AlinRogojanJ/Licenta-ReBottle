using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using ReBottle.Models;
using ReBottle.Models.DTOs;

namespace ReBottle.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> AddUserAsync(UserDTO request);
        Task<string?> LoginAsync(UserDTO request);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> UpdateUserAsync(Guid userId, JsonPatchDocument<UserUpdateDTO> patchDoc);
        Task DeleteUserAsync(Guid id);
    }
}
