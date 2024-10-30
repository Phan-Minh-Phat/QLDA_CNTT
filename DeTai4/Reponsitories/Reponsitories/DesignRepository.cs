using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeTai4.Repositories.Implementations
{
    public class DesignRepository : IDesignRepository
    {
        private readonly DeTai4Context _context;

        public DesignRepository(DeTai4Context context)
        {
            _context = context;
        }

        // Lấy tất cả các thiết kế
        public async Task<IEnumerable<Design>> GetAllDesignsAsync()
        {
            return await _context.Designs
                                 .Include(d => d.Projects)
                                 .ToListAsync();
        }

        // Lấy thiết kế theo ID
        public async Task<Design?> GetDesignByIdAsync(int designId)
        {
            return await _context.Designs
                                 .Include(d => d.Projects)
                                 .FirstOrDefaultAsync(d => d.DesignId == designId);
        }

        // Thêm thiết kế mới
        public async Task AddDesignAsync(Design design)
        {
            await _context.Designs.AddAsync(design);
            await _context.SaveChangesAsync();
        }

        // Cập nhật thông tin thiết kế
        public async Task UpdateDesignAsync(Design design)
        {
            _context.Designs.Update(design);
            await _context.SaveChangesAsync();
        }

        // Xóa thiết kế theo ID
        public async Task DeleteDesignAsync(int designId)
        {
            var design = await GetDesignByIdAsync(designId);
            if (design != null)
            {
                _context.Designs.Remove(design);
                await _context.SaveChangesAsync();
            }
        }

        // Lấy thiết kế theo tên
        public async Task<IEnumerable<Design>> GetDesignsByNameAsync(string designName)
        {
            return await _context.Designs
                                 .Include(d => d.Projects)
                                 .Where(d => d.DesignName != null && d.DesignName.Contains(designName))
                                 .ToListAsync();
        }
    }
}
