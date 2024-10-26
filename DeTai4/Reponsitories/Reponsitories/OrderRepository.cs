using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DeTai4Context _context;

        public OrderRepository(DeTai4Context context)
        {
            _context = context;
        }

        // Lấy tất cả các đơn hàng
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                                 .Include(o => o.Customer)
                                 .Include(o => o.Service)
                                 .Include(o => o.Feedbacks)
                                 .Include(o => o.Invoices)
                                 .ToListAsync();
        }

        // Lấy đơn hàng theo ID
        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                                 .Include(o => o.Customer)
                                 .Include(o => o.Service)
                                 .Include(o => o.Feedbacks)
                                 .Include(o => o.Invoices)
                                 .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        // Thêm đơn hàng mới
        public async Task AddOrderAsync(Order order)
        {
            var customer = await _context.Customers
                              .Include(c => c.User)
                              .FirstOrDefaultAsync(c => c.CustomerId == order.CustomerId);
            if (customer != null && customer.User != null)
            {
                order.FullName = customer.User.FullName;
            }
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        // Cập nhật thông tin đơn hàng
        public async Task UpdateOrderAsync(Order order)
        {
            var customer = await _context.Customers
                                 .Include(c => c.User)
                                 .FirstOrDefaultAsync(c => c.CustomerId == order.CustomerId);
            if (customer != null && customer.User != null)
            {
                order.FullName = customer.User.FullName;
            }
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        // Xóa đơn hàng theo ID
        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await GetOrderByIdAsync(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        // Lấy đơn hàng theo CustomerId
        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _context.Orders
                                 .Include(o => o.Customer)
                                 .Include(o => o.Service)
                                 .Include(o => o.Feedbacks)
                                 .Include(o => o.Invoices)
                                 .Where(o => o.CustomerId == customerId)
                                 .ToListAsync();
        }

        // Lấy đơn hàng theo trạng thái
        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status)
        {
            return await _context.Orders
                                 .Include(o => o.Customer)
                                 .Include(o => o.Service)
                                 .Include(o => o.Feedbacks)
                                 .Include(o => o.Invoices)
                                 .Where(o => o.Status == status)
                                 .ToListAsync();
        }
    }
}
