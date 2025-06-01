using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ReBottle.Models;
using ReBottle.Models.Data;
using ReBottle.Models.DTOs;
using ReBottle.Services.Interfaces;

namespace ReBottle.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ReBottleContext _reBottleContext;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IMapper mapper, ReBottleContext reBottleContext, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _reBottleContext = reBottleContext;
            _configuration = configuration;
        }

        public async Task<IEnumerable<UserGetDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserGetDTO>>(users);
        }

        public async Task<UserGetDTO> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return _mapper.Map<UserGetDTO>(user);
        }

        public async Task<User?> AddUserAsync(UserDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                
                return null;
            }

            var user = new User
            {
                UserId = Guid.NewGuid(),
                Username = request.Username,
                Email = request.Email,
                Phone = request.Phone,
                Password = request.Password,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                IsActive = true,
                Avatar = request.Avatar,
                Name = request.Name,
            };
            var hashedPassword = new PasswordHasher<User>()
                .HashPassword(user, request.Password);

            user.Username = request.Username;
            user.Password = hashedPassword;

            await _userRepository.AddUserAsync(user);

            return user;

        }

        public async Task<string?> LoginAsync(LoginDTO request)
        {
            var user = await _reBottleContext.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user is null) return null;
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, request.Password) ==
                PasswordVerificationResult.Failed) return null;

           return CreateToken(user);
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userRepository.DeleteUserAsync(id);
        }

        public async Task<User> UpdateUserAsync(Guid userId, JsonPatchDocument<UserDTO> patchDoc)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new Exception($"User not found with id: {userId}");
            }

            var userDto = _mapper.Map<UserDTO>(user);

            patchDoc.ApplyTo(userDto);
            _mapper.Map(userDto, user);

            user.Updated = DateTime.Now;

            await _userRepository.UpdateUserAsync(user);
            return user;
        }

        public async Task<bool> ForgotPasswordAsync(string email)
        {
            var user = await _reBottleContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return false;

            // Generate password reset token (short expiry token)
            var token = CreateToken(user, expiresInMinutes: 15); // Reuse CreateToken, adjust as needed

            var resetLink = $"https://localhost:7179/api/User/reset-password?token={token}";
            var subject = "Reset Your Password";
            var body = $@"
    <html>
        <body>
            <p>Click the link below to reset your password:</p>
            <p><a href=""{resetLink}"" target=""_blank"">{resetLink}</a></p>
        </body>
    </html>";


            var emailSender = new EmailSender(_configuration);
            await emailSender.SendEmailAsync(email, subject, body);

            return true;
        }

        private string CreateToken(User user, int expiresInMinutes = 60 * 24)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("Email", user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer: _configuration["AppSettings:Issuer"],
                audience: _configuration["AppSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiresInMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> ResetPasswordAsync(string token, string newPassword)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"]!);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _configuration["AppSettings:Issuer"],
                    ValidAudience = _configuration["AppSettings:Audience"],
                    ClockSkew = TimeSpan.Zero // no buffer
                }, out SecurityToken validatedToken);

                var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId)) return false;

                var user = await _reBottleContext.Users.FirstOrDefaultAsync(u => u.UserId.ToString() == userId);
                if (user == null) return false;

                var hashedPassword = new PasswordHasher<User>().HashPassword(user, newPassword);
                user.Password = hashedPassword;
                user.Updated = DateTime.Now;

                await _reBottleContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }



    }
}
