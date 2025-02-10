using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReBottle.Models;
using ReBottle.Models.DTOs;

namespace ReBottle.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO?> GetUserByIdAsync(string id);
        Task AddUserAsync(User user);
    }
}
