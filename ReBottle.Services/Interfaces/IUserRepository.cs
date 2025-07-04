﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReBottle.Models;

namespace ReBottle.Services.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User?> AddUserAsync(User user);

        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid id);
    }
}
