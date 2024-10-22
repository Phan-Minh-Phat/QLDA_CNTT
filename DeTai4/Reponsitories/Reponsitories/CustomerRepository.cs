using DeTai4.Repositories.Entities; // Tham chiếu đúng đến các entity
using Microsoft.EntityFrameworkCore;
using DeTai4.Repositories.Interfaces; // Đảm bảo sử dụng đúng namespace cho ICustomerRepository
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Repositories.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DeTai4Context _context;

        public CustomerRepository(DeTai4Context context)
        {
            _context = context;
        }

        // Lấy tất cả khách hàng
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers
                                 .Include(c => c.User)
                                 .Include(c => c.Orders)
                                 .Include(c => c.Projects)
                                 .ToListAsync();
        }

        // Lấy khách hàng theo ID
        public async Task<Customer?> GetCustomerByIdAsync(int customerId)
        {
            return await _context.Customers
                                 .Include(c => c.User)
                                 .Include(c => c.Orders)
                                 .Include(c => c.Projects)
                                 .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        // Thêm khách hàng mới
        public async Task AddCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        // Cập nhật thông tin khách hàng
        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        // Xóa khách hàng theo ID
        public async Task DeleteCustomerAsync(int customerId)
        {
            var customer = await GetCustomerByIdAsync(customerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        // Lấy khách hàng theo UserId
        public async Task<Customer?> GetCustomerByUserIdAsync(int userId)
        {
            return await _context.Customers
                                 .Include(c => c.User)
                                 .FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
