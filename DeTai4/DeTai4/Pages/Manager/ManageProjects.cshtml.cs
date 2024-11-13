using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Services;
using DeTai4.Services.Interfaces;
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
    public class ManageProjectsModel : PageModel
    {
        private readonly IProjectService _projectService;

        public ManageProjectsModel(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public List<Project> Projects { get; set; } = new List<Project>();

        [BindProperty]
        public int ProjectId { get; set; }

        [BindProperty]
        public string Status { get; set; }

        [BindProperty]
        public string ProgressDetails { get; set; }

        public async Task OnGetAsync()
        {
            // Lấy danh sách tất cả dự án thi công
            Projects = (await _projectService.GetAllProjectsAsync()).Where(p => p.Status != "Completed").ToList();
        }

        public async Task<IActionResult> OnPostUpdateProgressAsync(int projectId, string status, string progressDetails)
        {
            var project = await _projectService.GetProjectByIdAsync(projectId);
            if (project == null)
            {
                ModelState.AddModelError("", "Không tìm thấy dự án.");
                return Page();
            }

            // Cập nhật tiến độ và trạng thái của dự án
            project.Status = status;
            project.RequestDetails += "\nTiến độ: " + progressDetails;
            await _projectService.UpdateProjectAsync(project);

            // Quay lại trang hiện tại để cập nhật danh sách dự án
            return RedirectToPage();
        }
    }
}
