using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeTai4.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly DeTai4Context _context;

        public UserRepository(DeTai4Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Set<User>().ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Set<User>().FindAsync(userId);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task CreateUserAsync(User user)
        {
            // Thêm người dùng mới vào bảng User
            await _context.Set<User>().AddAsync(user);
            await _context.SaveChangesAsync();

            // Kiểm tra vai trò của người dùng và thêm vào bảng Customer hoặc Staff
            if (user.Role == "Customer")
            {
                // Thêm vào bảng Customer nếu vai trò là Customer
                var customer = new Customer
                {
                    UserId = user.UserId,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber
                };
                await _context.Set<Customer>().AddAsync(customer);
            }
            else if (user.Role == "ConsultingStaff" || user.Role == "DesignStaff" || user.Role == "ConstructionStaff")
            {
                // Thêm vào bảng Staff nếu vai trò là các vai trò của nhân viên
                var staff = new Staff
                {
                    UserId = user.UserId,
                    FullName = user.FullName,
                    Role = user.Role
                };
                await _context.Set<Staff>().AddAsync(staff);
            }

            // Lưu các thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Set<User>().Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                _context.Set<User>().Remove(user);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Staff?> GetStaffByUserIdAsync(int userId)
        {
            return await _context.Staff.FirstOrDefaultAsync(s => s.UserId == userId);
        }

        public async Task<Customer?> GetCustomerByUserIdAsync(int userId)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
