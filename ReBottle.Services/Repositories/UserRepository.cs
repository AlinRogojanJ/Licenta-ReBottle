using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReBottle.Models;
using ReBottle.Services.Interfaces;
using ReBottle.Models.Data;

namespace ReBottle.Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ReBottleContext _reBottleContext;

        public UserRepository(ReBottleContext reBottleContext)
        {
            _reBottleContext = reBottleContext;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _reBottleContext.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _reBottleContext.Users.FirstOrDefaultAsync(r => r.UserId == id);
        }

        public async Task AddUserAsync(User user)
        {
            //if (await _reBottleContext.Users.AnyAsync(u => u.Username == user.Username))
            //    return null;

            await _reBottleContext.Users.AddAsync(user);
            await _reBottleContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _reBottleContext.Users.FindAsync(id);

            if (user != null) _reBottleContext.Users.Remove(user);

            await _reBottleContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _reBottleContext.Users.Update(user);
            await _reBottleContext.SaveChangesAsync();
        }
    }
}
