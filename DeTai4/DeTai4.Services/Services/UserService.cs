using System.Collections.Generic;
using System.Threading.Tasks;
using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Repositories.Interfaces;
using DeTai4.Services.Interfaces;

namespace DeTai4.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
            // Perform any additional business logic or validation before creating the user
            await _userRepository.CreateUserAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            // Perform any additional business logic or validation before updating the user
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int userId)
        {
            // Perform any additional business logic or validation before deleting the user
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
            // Thực hiện mã hóa mật khẩu
            return password; // Cần thay thế bằng hàm mã hóa mạnh hơn trong thực tế
        }

        private bool VerifyPassword(string storedHash, string enteredPassword)
        {
            // Kiểm tra mật khẩu hash (ví dụ: sử dụng bcrypt hoặc SHA-256)
            return storedHash == enteredPassword;
        }
    }
}
