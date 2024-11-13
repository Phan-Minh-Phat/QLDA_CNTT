using DeTai4.Reponsitories.Interface;
using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Repositories.Implementations
{
    public class MaintenanceResultRepository : IMaintenanceResultRepository
    {
        private readonly DeTai4Context _context;

        public MaintenanceResultRepository(DeTai4Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MaintenanceResult>> GetAllMaintenanceResultsAsync()
        {
            return await _context.MaintenanceResults.Include(m => m.Service).Include(m => m.Staff).ToListAsync();
        }

        public async Task<MaintenanceResult?> GetMaintenanceResultByIdAsync(int maintenanceResultId)
        {
            return await _context.MaintenanceResults.Include(m => m.Service).Include(m => m.Staff)
                                                     .FirstOrDefaultAsync(m => m.MaintenanceResultId == maintenanceResultId);
        }

        public async Task AddMaintenanceResultAsync(MaintenanceResult maintenanceResult)
        {
            await _context.MaintenanceResults.AddAsync(maintenanceResult);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMaintenanceResultAsync(MaintenanceResult maintenanceResult)
        {
            _context.MaintenanceResults.Update(maintenanceResult);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMaintenanceResultAsync(int maintenanceResultId)
        {
            var result = await GetMaintenanceResultByIdAsync(maintenanceResultId);
            if (result != null)
            {
                _context.MaintenanceResults.Remove(result);
                await _context.SaveChangesAsync();
            }
        }
    }
}
