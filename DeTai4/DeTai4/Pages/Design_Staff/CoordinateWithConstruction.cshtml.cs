using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeTai4.Services;

namespace DeTai4.Pages.Design_Staff
{
    public class CoordinateWithConstructionModel : PageModel
    {
        private readonly IDesignService _designService;
        private readonly IStaffService _constructionStaffService;
        private readonly IProjectService _projectService;

        [BindProperty]
        public int SelectedDesignId { get; set; }

        [BindProperty]
        public int SelectedStaffId { get; set; }

        // Khởi tạo giá trị mặc định để tránh cảnh báo về việc không khởi tạo giá trị không null
        public IEnumerable<Design> AllDesigns { get; set; } = new List<Design>();
        public IEnumerable<Staff> AllStaff { get; set; } = new List<Staff>();

        // Constructor để tiêm service
        public CoordinateWithConstructionModel(
            IDesignService designService,
            IStaffService constructionStaffService,
            IProjectService projectService)
        {
            _designService = designService;
            _constructionStaffService = constructionStaffService;
            _projectService = projectService;
        }

        // Lấy tất cả các thiết kế và nhân viên thi công
        public async Task<IActionResult> OnGetAsync()
        {
            AllDesigns = await _designService.GetAllDesignsAsync();
            AllStaff = await _constructionStaffService.GetAllStaffAsync();
            return Page();
        }

        // Xử lý phối hợp khi người dùng chọn thiết kế và nhân viên thi công
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var design = await _designService.GetDesignByIdAsync(SelectedDesignId);
                    var staff = await _constructionStaffService.GetStaffByIdAsync(SelectedStaffId);

                    if (design == null || staff == null)
                    {
                        ViewData["ErrorMessage"] = "Mẫu thiết kế hoặc nhân viên thi công không tồn tại.";
                        return Page();
                    }

                    // Thêm công việc vào dự án
                    var project = new Project
                    {
                        DesignId = design.DesignId,
                        StaffId = staff.StaffId
                    };

                    await _projectService.CreateProjectAsync(project);

                    return RedirectToPage("/Design_Staff/ManageDesigns");
                }
                catch (System.Exception ex)
                {
                    ViewData["ErrorMessage"] = $"Lỗi xảy ra: {ex.Message}";
                    return Page();
                }
            }

            return Page();
        }
    }
}

