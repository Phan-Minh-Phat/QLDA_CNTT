using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Repositories.Interfaces;
using DeTai4.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DeTai4.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }

        public async Task CreateUserAsync(User user)
        {
            await _userRepository.CreateUserAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int userId)
        {
            await _userRepository.DeleteUserAsync(userId);
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user != null)
            {
                return VerifyPassword(user.PasswordHash, password);
            }
            return false;
        }

        public async Task<bool> CheckUserExistsAsync(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            return user != null;
        }

     

        private string HashPassword(string password)
        {
            return password; // Cần thay thế bằng hàm mã hóa mạnh hơn trong thực tế
        }

        private bool VerifyPassword(string storedHash, string enteredPassword)
        {
            return storedHash == enteredPassword;
        }
        public async Task<int?> GetStaffIdByUserIdAsync(int userId)
        {
            var staff = await _userRepository.GetStaffByUserIdAsync(userId); // Giả định bạn đã triển khai hàm này trong UserRepository
            return staff?.StaffId;
        }

        public async Task<int?> GetCustomerIdByUserIdAsync(int userId)
        {
            var customer = await _userRepository.GetCustomerByUserIdAsync(userId); // Giả định bạn đã triển khai hàm này trong UserRepository
            return customer?.CustomerId;
        }
    }
}
