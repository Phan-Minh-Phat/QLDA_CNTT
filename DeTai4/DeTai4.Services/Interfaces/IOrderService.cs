using DeTai4.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Services.Interfaces
{
    public interface IOrderService
    {
        // Lấy tất cả các đơn hàng
        Task<IEnumerable<Order>> GetAllOrdersAsync();

        // Lấy đơn hàng theo ID
        Task<Order?> GetOrderByIdAsync(int orderId);

        // Thêm đơn hàng mới
        Task AddOrderAsync(Order order);

        // Cập nhật thông tin đơn hàng
        Task UpdateOrderAsync(Order order);

        // Xóa đơn hàng theo ID
        Task DeleteOrderAsync(int orderId);

        // Lấy đơn hàng theo CustomerId
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId);

        // Lấy đơn hàng theo trạng thái
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status);
    }
}
