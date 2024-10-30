using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Repositories.Interfaces;
using DeTai4.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Services.Implementations
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        // Lấy tất cả nhân viên
        public async Task<IEnumerable<Staff>> GetAllStaffAsync()
        {
            return await _staffRepository.GetAllStaffAsync();
        }

        // Lấy nhân viên theo ID
        public async Task<Staff?> GetStaffByIdAsync(int staffId)
        {
            return await _staffRepository.GetStaffByIdAsync(staffId);
        }

        // Thêm nhân viên mới
        public async Task AddStaffAsync(Staff staff)
        {
            await _staffRepository.AddStaffAsync(staff);
        }

        // Cập nhật thông tin nhân viên
        public async Task UpdateStaffAsync(Staff staff)
        {
            await _staffRepository.UpdateStaffAsync(staff);
        }

        // Xóa nhân viên theo ID
        public async Task DeleteStaffAsync(int staffId)
        {
            await _staffRepository.DeleteStaffAsync(staffId);
        }

        // Lấy nhân viên theo vai trò
        public async Task<IEnumerable<Staff>> GetStaffByRoleAsync(string role)
        {
            return await _staffRepository.GetStaffByRoleAsync(role);
        }
    }
}
