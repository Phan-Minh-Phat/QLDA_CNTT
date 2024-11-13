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
    public class ManageConstructionProgressModel : PageModel
    {
        private readonly IProjectService _projectService;

        public ManageConstructionProgressModel(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public List<Project> Projects { get; set; } = new List<Project>();

        [BindProperty]
        public int ProjectId { get; set; }

        [BindProperty]
        public string NewStatus { get; set; }

        public async Task OnGetAsync()
        {
            // Lấy StaffId của nhân viên từ thông tin đăng nhập
            if (int.TryParse(User.FindFirst("StaffId")?.Value, out int staffId))
            {
                // Lấy các dự án mà nhân viên được phân công
                Projects = (await _projectService.GetProjectsForStaffAsync(staffId)).ToList();
            }
            else
            {
                ModelState.AddModelError("", "Không thể xác định nhân viên đăng nhập.");
            }
        }

        public async Task<IActionResult> OnPostUpdateProgressAsync(int projectId, string newStatus)
        {
            // Tìm dự án dựa trên ProjectId và StaffId của nhân viên đăng nhập
            if (!int.TryParse(User.FindFirst("StaffId")?.Value, out int staffId))
            {
                ModelState.AddModelError("", "Không thể xác định nhân viên đăng nhập.");
                return Page();
            }

            var project = await _projectService.GetProjectByIdAsync(projectId);
            if (project == null || project.StaffId != staffId)
            {
                ModelState.AddModelError("", "Không tìm thấy dự án hoặc bạn không được phân công vào dự án này.");
                return Page();
            }

            // Cập nhật trạng thái tiến độ thi công
            project.Status = newStatus;
            await _projectService.UpdateProjectAsync(project);

            TempData["SuccessMessage"] = "Cập nhật tiến độ thành công!";
            return RedirectToPage();
        }
    }
}
