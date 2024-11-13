using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using System;
using System.Threading.Tasks;
using DeTai4.Services;

namespace DeTai4.Pages.Consulting_Staff
{
    public class ConsultModel : PageModel
    {
        private readonly IProjectService _projectService;

        public ConsultModel(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [BindProperty]
        public Project Project { get; set; }

        [BindProperty]
        public string ConsultationNotes { get; set; } // Thêm thuộc tính ConsultationNotes

        [BindProperty]
        public DateTime? EndDate { get; set; } // Thêm thuộc tính EndDate để nhận giá trị từ form
        [BindProperty]
        public string Status { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Project = await _projectService.GetProjectByIdAsync(id);

            if (Project == null)
            {
                // Nếu không tìm thấy dự án, chuyển hướng đến trang NotFound
                return RedirectToPage("/NotFound");
            }

            EndDate = Project.EndDate; // Thiết lập EndDate hiện tại để hiển thị

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync(int id, string status)
        {
            // Tìm dự án theo ID
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return RedirectToPage("/NotFound");
            }

            // Cập nhật trạng thái, ngày kết thúc và lời tư vấn của dự án
            project.Status = status; // Đặt trạng thái mới
            project.EndDate = EndDate; // Cập nhật ngày kết thúc
            project.RequestDetails += "\n" + ConsultationNotes; // Thêm lời tư vấn vào RequestDetails

            // Gọi dịch vụ để lưu thay đổi vào cơ sở dữ liệu
            await _projectService.UpdateProjectAsync(project);

            return RedirectToPage("/Consulting_Staff/ProvideConsultation");
        }
    }
}
