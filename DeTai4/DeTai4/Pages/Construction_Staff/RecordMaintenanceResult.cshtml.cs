using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Services;

namespace DeTai4.Pages.Construction_Staff
{
    public class RecordMaintenanceResultModel : PageModel
    {
        private readonly IProjectService _projectService;
        private readonly IServiceService _serviceService;
        private readonly IMaintenanceResultService _maintenanceResultService;

        public RecordMaintenanceResultModel(
            IProjectService projectService,
            IServiceService serviceService,
            IMaintenanceResultService maintenanceResultService)
        {
            _projectService = projectService;
            _serviceService = serviceService;
            _maintenanceResultService = maintenanceResultService;
        }

        public List<Project> CompletedProjects { get; set; } = new List<Project>();
        public List<Service> Services { get; set; } = new List<Service>();

        [BindProperty]
        public int SelectedProjectId { get; set; }

        [BindProperty]
        public int SelectedServiceId { get; set; }

        [BindProperty]
        public string ResultDescription { get; set; }

        public async Task OnGetAsync()
        {
            // Lấy danh sách các dự án hoàn thành và dịch vụ bảo dưỡng
            CompletedProjects = (await _projectService.GetCompletedProjectsAsync()).ToList();
            Services = (await _serviceService.GetAllServicesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (SelectedProjectId == 0 || SelectedServiceId == 0 || string.IsNullOrWhiteSpace(ResultDescription))
            {
                ModelState.AddModelError("", "Vui lòng chọn dự án, dịch vụ và nhập mô tả kết quả.");
                return Page();
            }

            // Lấy StaffId của nhân viên đang đăng nhập từ Claims
            if (!int.TryParse(User.FindFirst("StaffId")?.Value, out int staffId))
            {
                ModelState.AddModelError("", "Không thể xác định nhân viên đăng nhập.");
                return Page();
            }

            // Tạo đối tượng MaintenanceResult để ghi nhận kết quả bảo dưỡng
            var maintenanceResult = new MaintenanceResult
            {
                ProjectId = SelectedProjectId,
                ServiceId = SelectedServiceId,
                StaffId = staffId, // Gán StaffId của nhân viên đang đăng nhập vào đây
                ResultDescription = ResultDescription,
                ResultDate = DateTime.Now
            };

            await _maintenanceResultService.AddMaintenanceResultAsync(maintenanceResult);

            TempData["SuccessMessage"] = "Kết quả bảo dưỡng đã được ghi nhận thành công!";
            return RedirectToPage();
        }
    }
}
