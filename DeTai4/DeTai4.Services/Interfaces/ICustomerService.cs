using DeTai4.Reponsitories.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Services.Interfaces
{
    public interface ICustomerService
    {
        // Lấy tất cả khách hàng
        Task<IEnumerable<Customer>> GetAllCustomersAsync();

        // Lấy khách hàng theo ID
        Task<Customer?> GetCustomerByIdAsync(int customerId);

        // Thêm khách hàng mới
        Task AddCustomerAsync(Customer customer);

        // Cập nhật thông tin khách hàng
        Task UpdateCustomerAsync(Customer customer);

        // Xóa khách hàng theo ID
        Task DeleteCustomerAsync(int customerId);

        // Lấy khách hàng theo UserId
        Task<Customer?> GetCustomerByUserIdAsync(int userId);
    }
}
