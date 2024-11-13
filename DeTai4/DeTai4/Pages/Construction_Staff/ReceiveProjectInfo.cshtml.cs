using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DeTai4.Services;

namespace DeTai4.Pages.ConstructionStaff
{
    public class ReceiveProjectInfoModel : PageModel
    {
        private readonly IProjectService _projectService;

        public ReceiveProjectInfoModel(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public List<Project> AssignedProjects { get; set; } = new List<Project>();

        public async Task OnGetAsync()
        {
            // Lấy StaffId từ thông tin đăng nhập của nhân viên
            if (int.TryParse(User.FindFirst("StaffId")?.Value, out int staffId))
            {
                // Lấy danh sách các dự án được phân công cho nhân viên đăng nhập
                AssignedProjects = (await _projectService.GetProjectsForStaffAsync(staffId)).ToList();
            }
            else
            {
                ModelState.AddModelError("", "Không thể xác định nhân viên đăng nhập.");
            }
        }
    }
}
