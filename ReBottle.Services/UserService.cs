using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using ReBottle.Models;
using ReBottle.Models.DTOs;
using ReBottle.Services.Interfaces;

namespace ReBottle.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return _mapper.Map<User>(user);
        }

        public async Task AddUserAsync(User user)
        {
            await _userRepository.AddUserAsync(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userRepository.DeleteUserAsync(id);
        }

        public async Task<User> UpdateUserAsync(Guid userId, JsonPatchDocument<UserUpdateDTO> patchDoc)
        {
            // Retrieve the current user from the repository.
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new Exception($"User not found with id: {userId}");
            }

            // Map the current user state to the DTO using AutoMapper.
            var userDto = _mapper.Map<UserUpdateDTO>(user);

            // Apply the patch document to the DTO.
            patchDoc.ApplyTo(userDto);

            // Map the patched DTO back onto the user entity.
            _mapper.Map(userDto, user);

            // Update the audit timestamp.
            user.Updated = DateTime.Now;

            // Persist the changes.
            await _userRepository.UpdateUserAsync(user);
            return user;
        }

    }
}
