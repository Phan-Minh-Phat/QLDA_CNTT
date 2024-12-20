﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using DeTai4.Services;

namespace DeTai4.Pages.Customer
{
    public class SubmitProjectRequestModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly IProjectService _projectService;
        private readonly IDesignService _designService;

        public SubmitProjectRequestModel(ICustomerService customerService, IProjectService projectService, IDesignService designService)
        {
            _customerService = customerService;
            _projectService = projectService;
            _designService = designService;
        }

        [BindProperty]
        public string? RequestDetails { get; set; }

        [BindProperty]
        public int DesignId { get; set; }

        public List<Design> Designs { get; set; } = new List<Design>();

        public bool IsRequestSubmitted { get; set; }

        public async Task OnGetAsync()
        {
            // Lấy tất cả các mẫu thiết kế để hiển thị trong form cho khách hàng chọn
            Designs = (await _designService.GetAllDesignsAsync()).ToList();

            // Kiểm tra nếu TempData có chứa thông báo thành công
            if (TempData.ContainsKey("IsRequestSubmitted"))
            {
                IsRequestSubmitted = true;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Lấy UserId từ claims
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

            // Kiểm tra thông tin gửi yêu cầu
            if (string.IsNullOrWhiteSpace(RequestDetails) || DesignId <= 0)
            {
                ModelState.AddModelError("", "Thông tin yêu cầu không hợp lệ.");
                return Page();
            }

            // Tạo mới đối tượng Project chỉ với CustomerId
            var newProject = new Project
            {
                CustomerId = customer.CustomerId, // Chỉ lưu CustomerId
                DesignId = DesignId,
                ProjectName = "Yêu cầu thi công từ khách hàng",
                Status = "Pending",
                RequestDetails = RequestDetails,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(2)
            };

            try
            {
                // Gọi service để lưu thông tin project mới vào cơ sở dữ liệu
                await _projectService.CreateProjectAsync(newProject);
                TempData["SuccessMessage"] = "Quý khách đã gửi yêu cầu thi công thành công!";
                IsRequestSubmitted = true;
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Đã xảy ra lỗi khi gửi yêu cầu: {ex.Message}");
                return Page();
            }
        }
    }
}
