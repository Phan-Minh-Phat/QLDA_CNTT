using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace DeTai4.Pages.Design_Staff
{
    public class CreateDesignModel : PageModel
    {
        private readonly IDesignService _designService;

        [BindProperty]
        public Design NewDesign { get; set; }

        // Constructor to inject service
        public CreateDesignModel(IDesignService designService)
        {
            _designService = designService;
            // Khởi tạo NewDesign trong constructor để tránh lỗi "Non-nullable property must contain a non-null value"
            NewDesign = new Design
            {
                DesignName = string.Empty, // Gán giá trị mặc định cho thuộc tính không nullable
                Description = string.Empty,
                Cost = 0m
            };
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Kiểm tra xem ModelState có hợp lệ không
            if (ModelState.IsValid)
            {
                // Kiểm tra tên thiết kế (DesignName) không được rỗng
                if (string.IsNullOrEmpty(NewDesign.DesignName))
                {
                    ModelState.AddModelError("NewDesign.DesignName", "Design Name cannot be empty.");
                    return Page();
                }

                try
                {
                    // Thêm thiết kế mới vào cơ sở dữ liệu
                    await _designService.AddDesignAsync(NewDesign);
                    return RedirectToPage("/Design_Staff/ManageDesigns");
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu có
                    ViewData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                    return Page();
                }
            }

            return Page();
        }
    }
}

