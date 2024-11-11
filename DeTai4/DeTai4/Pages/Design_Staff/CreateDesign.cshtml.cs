using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace DeTai4.Pages.Design_Staff
{
    public class CreateDesignModel : PageModel
    {
        private readonly IDesignService _designService;

        // Dữ liệu thiết kế mới
        [BindProperty]
        public Design NewDesign { get; set; } = new Design();

        public CreateDesignModel(IDesignService designService)
        {
            _designService = designService;
        }

        // Xử lý GET
        public void OnGet()
        {
        }

        // Xử lý POST để tạo thiết kế mới
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Thay vì gọi CreateDesignAsync, gọi AddDesignAsync
                await _designService.AddDesignAsync(NewDesign);

                // Chuyển hướng về trang quản lý thiết kế hoặc trang khác sau khi thêm thành công
                return RedirectToPage("/Design_Staff/ManageDesigns");
            }
            catch (System.Exception)
            {
                // Hiển thị thông báo lỗi nếu có sự cố khi thêm thiết kế
                ViewData["ErrorMessage"] = "There was an error creating the design.";
                return Page();
            }
        }
    }
}
