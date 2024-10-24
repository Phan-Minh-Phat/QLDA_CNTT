using DeTai4.Repositories.Entities;
using DeTai4.Repositories.Interfaces;
using DeTai4.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Services.Implementations
{
    public class DesignService : IDesignService
    {
        private readonly IDesignRepository _designRepository;

        public DesignService(IDesignRepository designRepository)
        {
            _designRepository = designRepository;
        }

        // Lấy tất cả các thiết kế
        public async Task<IEnumerable<Design>> GetAllDesignsAsync()
        {
            return await _designRepository.GetAllDesignsAsync();
        }

        // Lấy thiết kế theo ID
        public async Task<Design?> GetDesignByIdAsync(int designId)
        {
            return await _designRepository.GetDesignByIdAsync(designId);
        }

        // Thêm thiết kế mới
        public async Task AddDesignAsync(Design design)
        {
            await _designRepository.AddDesignAsync(design);
        }

        // Cập nhật thông tin thiết kế
        public async Task UpdateDesignAsync(Design design)
        {
            await _designRepository.UpdateDesignAsync(design);
        }

        // Xóa thiết kế theo ID
        public async Task DeleteDesignAsync(int designId)
        {
            await _designRepository.DeleteDesignAsync(designId);
        }

        // Lấy thiết kế theo tên
        public async Task<IEnumerable<Design>> GetDesignsByNameAsync(string designName)
        {
            return await _designRepository.GetDesignsByNameAsync(designName);
        }
    }
}
