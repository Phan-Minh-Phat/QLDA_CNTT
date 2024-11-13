using DeTai4.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeTai4.Reponsitories.Repositories.Entities;
using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using DeTai4.Services;

namespace DeTai4.Pages.Customer
{
    public class OnlinePaymentModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IInvoiceService _invoiceService;
        private readonly ICustomerService _customerService;

        public OnlinePaymentModel(IOrderService orderService, IInvoiceService invoiceService, ICustomerService customerService)
        {
            _orderService = orderService;
            _invoiceService = invoiceService;
            _customerService = customerService;
        }

        public List<Order> Orders { get; set; } = new List<Order>();

        [BindProperty]
        public int SelectedOrderId { get; set; }

        [BindProperty]
        public string PaymentMethod { get; set; }

        public string PaymentStatus { get; set; }
        public bool IsPaymentSuccessful { get; set; }
        public DateTime? PaymentDate { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Lấy UserId từ Claims
            if (!int.TryParse(User.FindFirst("UserId")?.Value, out int userId))
            {
                ModelState.AddModelError("", "Không thể xác định UserId.");
                return Page();
            }

            // Lấy thông tin khách hàng dựa vào UserId
            var customer = await _customerService.GetCustomerByUserIdAsync(userId);
            if (customer == null)
            {
                ModelState.AddModelError("", "Không tìm thấy thông tin khách hàng.");
                return Page();
            }

            // Lấy các đơn hàng có trạng thái Pending của khách hàng này
            Orders = (await _orderService.GetOrdersByCustomerIdAsync(customer.CustomerId))
                .Where(o => o.Status == "Pending")
                .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Lấy UserId từ Claims
            if (!int.TryParse(User.FindFirst("UserId")?.Value, out int userId))
            {
                ModelState.AddModelError("", "Không thể xác định UserId.");
                await OnGetAsync(); // Lấy lại danh sách đơn hàng nếu lỗi
                return Page();
            }

            if (SelectedOrderId <= 0 || string.IsNullOrEmpty(PaymentMethod))
            {
                ModelState.AddModelError("", "Vui lòng chọn đơn hàng và phương thức thanh toán.");
                await OnGetAsync(); // Lấy lại danh sách đơn hàng nếu lỗi
                return Page();
            }

            // Lấy thông tin khách hàng dựa vào UserId
            var customer = await _customerService.GetCustomerByUserIdAsync(userId);
            if (customer == null)
            {
                ModelState.AddModelError("", "Không tìm thấy thông tin khách hàng.");
                await OnGetAsync(); // Lấy lại danh sách đơn hàng nếu lỗi
                return Page();
            }

            // Lấy đơn hàng từ danh sách đơn hàng của khách hàng
            Orders = (await _orderService.GetOrdersByCustomerIdAsync(customer.CustomerId)).ToList();
            var order = Orders.FirstOrDefault(o => o.OrderId == SelectedOrderId && o.Status == "Pending");
            if (order == null)
            {
                ModelState.AddModelError("", "Đơn hàng không hợp lệ.");
                await OnGetAsync(); // Lấy lại danh sách đơn hàng nếu lỗi
                return Page();
            }

            // Gán thời gian thanh toán nếu chưa có
            PaymentDate ??= DateTime.Now;

            var invoice = new Invoice
            {
                OrderId = SelectedOrderId,
                CustomerId = customer.CustomerId,
                TotalAmount = order.TotalCost ?? 0,
                PaymentMethod = PaymentMethod,
                PaymentDate = PaymentDate.Value,
                PaymentStatus = "Completed"
            };

            try
            {
                // Tạo hóa đơn và cập nhật trạng thái đơn hàng
                await _invoiceService.CreateInvoiceAsync(invoice);
                order.Status = "Paid";
                await _orderService.UpdateOrderAsync(order);

                IsPaymentSuccessful = true;
                PaymentStatus = "Success";
                TempData["ThankYouMessage"] = $"Cảm ơn quý khách đã thanh toán vào ngày {PaymentDate.Value:dd/MM/yyyy HH:mm:ss}!";
                return RedirectToPage("/Customer/OnlinePayment"); // Redirect để làm mới trang
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Đã xảy ra lỗi khi thanh toán: {ex.Message}");
                PaymentStatus = "Failed";
                await OnGetAsync(); // Lấy lại danh sách đơn hàng nếu lỗi
                return Page();
            }
        }
    }
}
