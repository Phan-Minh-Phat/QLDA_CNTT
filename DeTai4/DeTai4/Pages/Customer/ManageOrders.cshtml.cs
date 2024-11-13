using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using DeTai4.Services;

namespace DeTai4.Pages.Customer
{
    public class ManageProjectsModel : PageModel
    {
        private readonly IProjectService _projectService;
        private readonly ICustomerService _customerService;

        public ManageProjectsModel(IProjectService projectService, ICustomerService customerService)
        {
            _projectService = projectService;
            _customerService = customerService;
        }

        public List<Project> Projects { get; set; } = new List<Project>();

        public async Task<IActionResult> OnGetAsync()
        {
            // Lấy UserId từ Claims sau khi đăng nhập
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

            // Lấy tất cả các dự án của khách hàng
            Projects = (await _projectService.GetProjectsByCustomerIdAsync(customer.CustomerId)).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostCancelProjectAsync(int projectId)
        {
            // Tìm dự án theo ID
            var project = await _projectService.GetProjectByIdAsync(projectId);
            if (project == null)
            {
                ModelState.AddModelError("", "Không tìm thấy dự án.");
                return RedirectToPage();
            }

            // Cập nhật trạng thái dự án thành "Cancelled"
            project.Status = "Cancelled";
            await _projectService.UpdateProjectAsync(project);

            return RedirectToPage();
        }
    }
}
