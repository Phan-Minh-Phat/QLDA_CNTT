using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeTai4.Services;

namespace DeTai4.Pages.Manager
{
    public class ManageStaffModel : PageModel
    {
        private readonly IStaffService _staffService;
        private readonly IProjectService _projectService;

        public ManageStaffModel(IStaffService staffService, IProjectService projectService)
        {
            _staffService = staffService;
            _projectService = projectService;
        }

        public List<Staff> Staffs { get; set; } = new List<Staff>();
        public List<Project> Projects { get; set; } = new List<Project>();

        [BindProperty]
        public int SelectedStaffId { get; set; }

        [BindProperty]
        public int SelectedProjectId { get; set; }

        public async Task OnGetAsync()
        {
            // Lấy danh sách nhân viên
            Staffs = (await _staffService.GetAllStaffAsync()).ToList();

            // Lấy danh sách dự án chưa phân công
            Projects = (await _projectService.GetAllProjectsAsync()).Where(p => p.StaffId == null).ToList();
        }
 
        public async Task<IActionResult> OnPostAssignStaffAsync(int selectedProjectId, int selectedStaffId)
        {
            // Xác định dự án cần phân công
            var project = await _projectService.GetProjectByIdAsync(selectedProjectId);
            if (project == null)
            {
                ModelState.AddModelError("", "Không tìm thấy dự án.");
                return Page();
            }

            // Cập nhật StaffId của dự án
            project.StaffId = selectedStaffId;
            await _projectService.UpdateProjectAsync(project);

            // Quay lại trang hiện tại để cập nhật danh sách
            return RedirectToPage();
        }
    }
}
