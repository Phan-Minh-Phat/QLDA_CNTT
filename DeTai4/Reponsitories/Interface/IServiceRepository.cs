using DeTai4.Reponsitories.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        // Lấy tất cả các dịch vụ
        Task<IEnumerable<Service>> GetAllServicesAsync();

        // Lấy dịch vụ theo ID
        Task<Service?> GetServiceByIdAsync(int serviceId);

        // Thêm dịch vụ mới
        Task AddServiceAsync(Service service);

        // Cập nhật thông tin dịch vụ
        Task UpdateServiceAsync(Service service);

        // Xóa dịch vụ theo ID
        Task DeleteServiceAsync(int serviceId);

        // Lấy dịch vụ theo tên
        Task<IEnumerable<Service>> GetServicesByNameAsync(string serviceName);
    }
}
