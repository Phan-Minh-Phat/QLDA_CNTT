using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Repositories.Interfaces;
using DeTai4.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Services.Implementations
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        // Lấy tất cả các dịch vụ
        public async Task<IEnumerable<Service>> GetAllServicesAsync()
        {
            return await _serviceRepository.GetAllServicesAsync();
        }

        // Lấy dịch vụ theo ID
        public async Task<Service?> GetServiceByIdAsync(int serviceId)
        {
            return await _serviceRepository.GetServiceByIdAsync(serviceId);
        }

        // Thêm dịch vụ mới
        public async Task AddServiceAsync(Service service)
        {
            await _serviceRepository.AddServiceAsync(service);
        }

        // Cập nhật thông tin dịch vụ
        public async Task UpdateServiceAsync(Service service)
        {
            await _serviceRepository.UpdateServiceAsync(service);
        }

        // Xóa dịch vụ theo ID
        public async Task DeleteServiceAsync(int serviceId)
        {
            await _serviceRepository.DeleteServiceAsync(serviceId);
        }

        // Lấy dịch vụ theo tên
        public async Task<IEnumerable<Service>> GetServicesByNameAsync(string serviceName)
        {
            return await _serviceRepository.GetServicesByNameAsync(serviceName);
        }
    }
}
