using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeTai4.Services;

namespace DeTai4.Pages.Customer
{
    public class SubmitFeedbackModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IFeedbackService _feedbackService;

        public SubmitFeedbackModel(IOrderService orderService, ICustomerService customerService, IFeedbackService feedbackService)
        {
            _orderService = orderService;
            _customerService = customerService;
            _feedbackService = feedbackService;
        }

        [BindProperty]
        public int SelectedOrderId { get; set; } // ID của đơn hàng được chọn

        [BindProperty]
        public int? Rating { get; set; } // Đánh giá (số sao)

        [BindProperty]
        public string? Comment { get; set; } // Bình luận

        public bool IsFeedbackSubmitted { get; set; } // Để hiển thị thông báo sau khi gửi phản hồi

        public List<Order> Orders { get; set; } = new List<Order>();
        public DateTime FeedbackSubmissionTime { get; set; } // Thời gian gửi phản hồi

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

            // Lấy các đơn hàng của khách hàng này
            Orders = (await _orderService.GetOrdersByCustomerIdAsync(customer.CustomerId)).ToList();

            if (TempData.ContainsKey("FeedbackMessage"))
            {
                IsFeedbackSubmitted = true;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Lấy UserId từ Claims
            if (!int.TryParse(User.FindFirst("UserId")?.Value, out int userId))
            {
                ModelState.AddModelError("", "Không thể xác định UserId.");
                return await OnGetAsync();
            }

            if (SelectedOrderId == 0 || Rating == null)
            {
                ModelState.AddModelError("", "Vui lòng chọn đơn hàng và đánh giá.");
                return await OnGetAsync();
            }

            // Lấy thông tin khách hàng dựa vào UserId
            var customer = await _customerService.GetCustomerByUserIdAsync(userId);
            if (customer == null)
            {
                ModelState.AddModelError("", "Không tìm thấy thông tin khách hàng.");
                return await OnGetAsync();
            }

            // Lấy các đơn hàng của khách hàng này
            Orders = (await _orderService.GetOrdersByCustomerIdAsync(customer.CustomerId)).ToList();

            // Kiểm tra xem SelectedOrderId có nằm trong danh sách đơn hàng của khách hàng không
            var order = Orders.FirstOrDefault(o => o.OrderId == SelectedOrderId);
            if (order == null)
            {
                ModelState.AddModelError("", "Đơn hàng không hợp lệ.");
                return await OnGetAsync();
            }

            // Lưu phản hồi vào cơ sở dữ liệu
            FeedbackSubmissionTime = DateTime.Now;
            var feedback = new Feedback
            {
                OrderId = SelectedOrderId,
                Rating = Rating,
                Comment = Comment,
                FeedbackDate = FeedbackSubmissionTime
            };

            try
            {
                await _feedbackService.CreateFeedbackAsync(feedback);
                TempData["FeedbackMessage"] = $"Cảm ơn quý khách đã gửi phản hồi vào {FeedbackSubmissionTime:dd/MM/yyyy HH:mm:ss}!";
                return RedirectToPage("/Customer/SubmitFeedback");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Đã xảy ra lỗi khi lưu phản hồi: {ex.Message}");
                return await OnGetAsync();
            }
        }

    }
}
