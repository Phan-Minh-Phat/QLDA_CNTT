using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using DeTai4.Services;

namespace DeTai4.Pages.ConstructionStaff
{
    public class HandOverProjectModel : PageModel
    {
        private readonly IProjectService _projectService;
        private readonly IFeedbackService _feedbackService;

        public HandOverProjectModel(IProjectService projectService, IFeedbackService feedbackService)
        {
            _projectService = projectService;
            _feedbackService = feedbackService;
        }

        public List<Project> ProjectsToHandOver { get; set; } = new List<Project>();

        public async Task OnGetAsync()
        {
            // Lấy StaffId của nhân viên từ thông tin đăng nhập
            if (int.TryParse(User.FindFirst("StaffId")?.Value, out int staffId))
            {
                // Lấy các dự án mà nhân viên được phân công với trạng thái "In Progress"
                ProjectsToHandOver = (await _projectService.GetProjectsForStaffAsync(staffId))
                                     .Where(p => p.Status == "In Progress")
                                     .ToList();
            }
            else
            {
                ModelState.AddModelError("", "Không thể xác định nhân viên đăng nhập.");
            }
        }

        public async Task<IActionResult> OnPostHandOverProjectAsync(int projectId)
        {
            // Lấy dự án để bàn giao
            var project = await _projectService.GetProjectByIdAsync(projectId);
            if (project == null || project.Status != "In Progress")
            {
                ModelState.AddModelError("", "Không tìm thấy dự án hoặc dự án không đủ điều kiện để bàn giao.");
                return Page();
            }

            // Cập nhật trạng thái dự án thành "Completed"
            project.Status = "Completed";
            await _projectService.UpdateProjectAsync(project);

            TempData["SuccessMessage"] = "Bàn giao công trình thành công!";

            // Chuyển hướng để làm mới trang sau khi bàn giao
            return RedirectToPage();
        }
    }
}
