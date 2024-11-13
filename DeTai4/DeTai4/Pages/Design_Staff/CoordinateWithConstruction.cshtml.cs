using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeTai4.Services;

namespace DeTai4.Pages.Design_Staff
{
    public class CoordinateWithConstructionModel : PageModel
    {
        private readonly IProjectService _projectService;
        private readonly IStaffService _staffService;

        public CoordinateWithConstructionModel(IProjectService projectService, IStaffService staffService)
        {
            _projectService = projectService;
            _staffService = staffService;
        }

        public List<Project> Projects { get; set; } = new List<Project>();
        public List<Staff> ConstructionStaffs { get; set; } = new List<Staff>();

        [BindProperty]
        public int SelectedStaffId { get; set; }

        [BindProperty]
        public int SelectedProjectId { get; set; }

        [BindProperty]
        public string AdditionalInfo { get; set; } // Thông tin bổ sung

        public async Task OnGetAsync()
        {
            // Lấy danh sách các dự án cần phối hợp
            Projects = (List<Project>)await _projectService.GetAllProjectsAsync();

            // Lấy danh sách các nhân viên thi công (Construction role)
            ConstructionStaffs = (await _staffService.GetStaffByRoleAsync("ConstructionStaff")).ToList();
        }

        public async Task<IActionResult> OnPostAssignStaffAsync(int selectedProjectId, int selectedStaffId, string additionalInfo)
        {
            // Xác định dự án cần phân công
            var project = await _projectService.GetProjectByIdAsync(selectedProjectId);
            if (project == null)
            {
                ModelState.AddModelError("", "Không tìm thấy dự án.");
                return Page();
            }

            // Cập nhật StaffId và thông tin bổ sung cho dự án
            project.StaffId = selectedStaffId;
            project.RequestDetails += "\nThông tin bổ sung: " + additionalInfo;
            await _projectService.UpdateProjectAsync(project);

            // Quay lại trang hiện tại để cập nhật danh sách
            return RedirectToPage();
        }
    }
}
