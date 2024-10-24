using DeTai4.Repositories.Entities;
using DeTai4.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeTai4.Repositories.Implementations
{
    public class StaffRepository : IStaffRepository
    {
        private readonly DeTai4Context _context;

        public StaffRepository(DeTai4Context context)
        {
            _context = context;
        }

        // Lấy tất cả nhân viên
        public async Task<IEnumerable<Staff>> GetAllStaffAsync()
        {
            return await _context.Staff
                                 .Include(s => s.User)
                                 .Include(s => s.Projects)
                                 .Include(s => s.Services)
                                 .Include(s => s.MaintenanceResults)
                                 .ToListAsync();
        }

        // Lấy nhân viên theo ID
        public async Task<Staff?> GetStaffByIdAsync(int staffId)
        {
            return await _context.Staff
                                 .Include(s => s.User)
                                 .Include(s => s.Projects)
                                 .Include(s => s.Services)
                                 .Include(s => s.MaintenanceResults)
                                 .FirstOrDefaultAsync(s => s.StaffId == staffId);
        }

        // Thêm nhân viên mới
        public async Task AddStaffAsync(Staff staff)
        {
            await _context.Staff.AddAsync(staff);
            await _context.SaveChangesAsync();
        }

        // Cập nhật thông tin nhân viên
        public async Task UpdateStaffAsync(Staff staff)
        {
            _context.Staff.Update(staff);
            await _context.SaveChangesAsync();
        }

        // Xóa nhân viên theo ID
        public async Task DeleteStaffAsync(int staffId)
        {
            var staff = await GetStaffByIdAsync(staffId);
            if (staff != null)
            {
                _context.Staff.Remove(staff);
                await _context.SaveChangesAsync();
            }
        }

        // Lấy nhân viên theo vai trò
        public async Task<IEnumerable<Staff>> GetStaffByRoleAsync(string role)
        {
            return await _context.Staff
                                 .Include(s => s.User)
                                 .Include(s => s.Projects)
                                 .Include(s => s.Services)
                                 .Include(s => s.MaintenanceResults)
                                 .Where(s => s.Role != null && s.Role.Contains(role))
                                 .ToListAsync();
        }
    }
}
