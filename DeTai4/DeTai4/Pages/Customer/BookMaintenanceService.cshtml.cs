using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace DeTai4.Pages.Customer
{
    public class BookMaintenanceServiceModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        private readonly IServiceService _serviceService;

        public BookMaintenanceServiceModel(ICustomerService customerService, IOrderService orderService, IServiceService serviceService)
        {
            _customerService = customerService;
            _orderService = orderService;
            _serviceService = serviceService;
        }

        [BindProperty]
        public int ServiceId { get; set; }

        [BindProperty]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public List<Service> Services { get; set; } = new List<Service>();

        public async Task OnGetAsync()
        {
            // Lấy tất cả các dịch vụ bảo dưỡng để hiển thị trong form cho khách hàng chọn
            Services = (await _serviceService.GetAllServicesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!int.TryParse(User.FindFirst("UserId")?.Value, out int userId))
            {
                ModelState.AddModelError("", "Không thể xác định UserId.");
                Services = (await _serviceService.GetAllServicesAsync()).ToList();
                return Page();
            }

            var customer = await _customerService.GetCustomerByUserIdAsync(userId);
            if (customer == null)
            {
                ModelState.AddModelError("", "Không tìm thấy thông tin khách hàng.");
                Services = (await _serviceService.GetAllServicesAsync()).ToList();
                return Page();
            }

            if (ServiceId <= 0)
            {
                ModelState.AddModelError("", "Vui lòng chọn một dịch vụ.");
                Services = (await _serviceService.GetAllServicesAsync()).ToList();
                return Page();
            }

            var service = await _serviceService.GetServiceByIdAsync(ServiceId);
            if (service == null || !service.Cost.HasValue)
            {
                ModelState.AddModelError("", "Dịch vụ không hợp lệ hoặc không có giá.");
                Services = (await _serviceService.GetAllServicesAsync()).ToList();
                return Page();
            }

            var newOrder = new Order
            {
                CustomerId = customer.CustomerId,
                UserId = userId,
                ServiceId = ServiceId,
                OrderDate = OrderDate,
                Status = "Pending",
                TotalCost = service.Cost.Value
            };

            try
            {
                await _orderService.AddOrderAsync(newOrder);
                TempData["SuccessMessage"] = "Quý khách đã đặt dịch vụ thành công!";
                return RedirectToPage("/Customer/BookMaintenanceService");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Đã xảy ra lỗi khi đặt dịch vụ: {ex.Message}");
                Services = (await _serviceService.GetAllServicesAsync()).ToList();
                return Page();
            }
        }
    }
}
