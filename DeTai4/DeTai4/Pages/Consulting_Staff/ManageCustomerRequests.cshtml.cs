using Microsoft.AspNetCore.Mvc.RazorPages;
using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DeTai4.Services;

namespace DeTai4.Pages.Consulting_Staff
{
    public class ManageCustomerRequestsModel : PageModel
    {
        private readonly IProjectService _projectService;

        public ManageCustomerRequestsModel(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public List<Project> PendingProjects { get; set; } = new List<Project>();

        public async Task<IActionResult> OnGetAsync()
        {
            // Lấy vai trò của người dùng từ Claims
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var userIdClaim = User.FindFirst("UserId")?.Value;

            // Kiểm tra nếu người dùng là nhân viên tư vấn
            if (userRole == "ConsultingStaff" && int.TryParse(userIdClaim, out int staffId))
            {
                // Lấy danh sách dự án pending từ ProjectService
                PendingProjects = (await _projectService.GetPendingProjectsForStaffAsync(staffId)).ToList();

             
            }
            else
            {
                // Nếu không phải nhân viên tư vấn, chuyển hướng hoặc hiển thị thông báo lỗi
                return RedirectToPage("/AccessDenied");
            }

            return Page();
        }
    }
}



