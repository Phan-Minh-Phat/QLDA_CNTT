using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Repositories.Implementations
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly DeTai4Context _context;

        public ServiceRepository(DeTai4Context context)
        {
            _context = context;
        }

        // Lấy tất cả các dịch vụ
        public async Task<IEnumerable<Service>> GetAllServicesAsync()
        {
            return await _context.Services
                                 .Include(s => s.MaintenanceResults)
                                 .Include(s => s.Orders)
                                
                                 .ToListAsync();
        }

        // Lấy dịch vụ theo ID
        public async Task<Service?> GetServiceByIdAsync(int serviceId)
        {
            return await _context.Services
                                 .Include(s => s.MaintenanceResults)
                                 .Include(s => s.Orders)
                                
                                 .FirstOrDefaultAsync(s => s.ServiceId == serviceId);
        }

        // Thêm dịch vụ mới
        public async Task AddServiceAsync(Service service)
        {
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
        }

        // Cập nhật thông tin dịch vụ
        public async Task UpdateServiceAsync(Service service)
        {
            _context.Services.Update(service);
            await _context.SaveChangesAsync();
        }

        // Xóa dịch vụ theo ID
        public async Task DeleteServiceAsync(int serviceId)
        {
            var service = await GetServiceByIdAsync(serviceId);
            if (service != null)
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
            }
        }

        // Lấy dịch vụ theo tên
        public async Task<IEnumerable<Service>> GetServicesByNameAsync(string serviceName)
        {
            return await _context.Services
                                 .Include(s => s.MaintenanceResults)
                                 .Include(s => s.Orders)
                                
                                 .Where(s => s.ServiceName != null && s.ServiceName.Contains(serviceName))
                                 .ToListAsync();
        }
    }
}
