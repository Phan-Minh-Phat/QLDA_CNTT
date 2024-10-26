using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Repositories.Interfaces;
using DeTai4.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // Lấy tất cả các đơn hàng
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        // Lấy đơn hàng theo ID
        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

        // Thêm đơn hàng mới
        public async Task AddOrderAsync(Order order)
        {
            await _orderRepository.AddOrderAsync(order);
        }

        // Cập nhật thông tin đơn hàng
        public async Task UpdateOrderAsync(Order order)
        {
            await _orderRepository.UpdateOrderAsync(order);
        }

        // Xóa đơn hàng theo ID
        public async Task DeleteOrderAsync(int orderId)
        {
            await _orderRepository.DeleteOrderAsync(orderId);
        }

        // Lấy đơn hàng theo CustomerId
        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _orderRepository.GetOrdersByCustomerIdAsync(customerId);
        }

        // Lấy đơn hàng theo trạng thái
        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status)
        {
            return await _orderRepository.GetOrdersByStatusAsync(status);
        }
    }
}
