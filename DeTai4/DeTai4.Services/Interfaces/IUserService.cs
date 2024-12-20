﻿using System.Collections.Generic;
using System.Threading.Tasks;
using DeTai4.Reponsitories.Repositories.Entities;

namespace DeTai4.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int userId);
        Task<User?> GetUserByUsernameAsync(string username);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
        Task<bool> ValidateUserAsync(string username, string password);
        Task<bool> CheckUserExistsAsync(string username);
        Task<int?> GetStaffIdByUserIdAsync(int userId);
        Task<int?> GetCustomerIdByUserIdAsync(int userId);


    }
}
