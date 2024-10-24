using DeTai4.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Services.Interfaces
{
    public interface IDesignService
    {
        // Lấy tất cả các thiết kế
        Task<IEnumerable<Design>> GetAllDesignsAsync();

        // Lấy thiết kế theo ID
        Task<Design?> GetDesignByIdAsync(int designId);

        // Thêm thiết kế mới
        Task AddDesignAsync(Design design);

        // Cập nhật thông tin thiết kế
        Task UpdateDesignAsync(Design design);

        // Xóa thiết kế theo ID
        Task DeleteDesignAsync(int designId);

        // Lấy thiết kế theo tên
        Task<IEnumerable<Design>> GetDesignsByNameAsync(string designName);
    }
}
