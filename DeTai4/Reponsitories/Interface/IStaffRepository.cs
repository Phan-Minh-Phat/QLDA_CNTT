using DeTai4.Reponsitories.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Repositories.Interfaces
{
    public interface IStaffRepository
    {
        // Lấy tất cả nhân viên
        Task<IEnumerable<Staff>> GetAllStaffAsync();

        // Lấy nhân viên theo ID
        Task<Staff?> GetStaffByIdAsync(int staffId);

        // Thêm nhân viên mới
        Task AddStaffAsync(Staff staff);

        // Cập nhật thông tin nhân viên
        Task UpdateStaffAsync(Staff staff);

        // Xóa nhân viên theo ID
        Task DeleteStaffAsync(int staffId);

        // Lấy nhân viên theo vai trò
        Task<IEnumerable<Staff>> GetStaffByRoleAsync(string role);
    }
}
