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
        Task<string?> LoginAsync(LoginDTO request);
        Task<IEnumerable<UserGetDTO>> GetAllUsersAsync();
        Task<UserGetDTO> GetUserByIdAsync(Guid id);
        Task<User> UpdateUserAsync(Guid userId, JsonPatchDocument<UserDTO> patchDoc);
        Task DeleteUserAsync(Guid id);
        Task<bool> ForgotPasswordAsync(string email);
        Task<bool> ResetPasswordAsync(string token, string newPassword);
    }
}
