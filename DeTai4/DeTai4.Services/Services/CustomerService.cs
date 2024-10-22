using System.Collections.Generic;
using System.Threading.Tasks;
using DeTai4.Repositories.Entities;
using DeTai4.Repositories.Interfaces;
using DeTai4.Services.Interfaces;

namespace DeTai4.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // Lấy tất cả khách hàng
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllCustomersAsync();
        }

        // Lấy khách hàng theo ID
        public async Task<Customer?> GetCustomerByIdAsync(int customerId)
        {
            return await _customerRepository.GetCustomerByIdAsync(customerId);
        }

        // Thêm khách hàng mới
        public async Task AddCustomerAsync(Customer customer)
        {
            await _customerRepository.AddCustomerAsync(customer);
        }

        // Cập nhật thông tin khách hàng
        public async Task UpdateCustomerAsync(Customer customer)
        {
            await _customerRepository.UpdateCustomerAsync(customer);
        }

        // Xóa khách hàng theo ID
        public async Task DeleteCustomerAsync(int customerId)
        {
            await _customerRepository.DeleteCustomerAsync(customerId);
        }

        // Lấy khách hàng theo UserId
        public async Task<Customer?> GetCustomerByUserIdAsync(int userId)
        {
            return await _customerRepository.GetCustomerByUserIdAsync(userId);
        }
    }
}
