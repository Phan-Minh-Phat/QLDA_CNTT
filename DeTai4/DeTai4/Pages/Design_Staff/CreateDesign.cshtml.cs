using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using System.Threading.Tasks;

namespace DeTai4.Pages.Design_Staff
{
    public class CreateDesignModel : PageModel
    {
        private readonly IDesignService _designService;

        public CreateDesignModel(IDesignService designService)
        {
            _designService = designService;
        }

        [BindProperty]
        public Design Design { get; set; } = new Design();

        [TempData]
        public string SuccessMessage { get; set; } // Thông báo thành công

        public void OnGet()
        {
            // Hiển thị form tạo mới
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Thêm thiết kế mới vào cơ sở dữ liệu
            await _designService.AddDesignAsync(Design);

            // Gán thông báo thành công vào TempData
            SuccessMessage = "Bạn đã lưu mẫu thiết kế thành công";

            // Chuyển hướng về trang hiện tại để hiển thị thông báo
            return RedirectToPage("/Design_Staff/CreateDesign");
        }
    }
}
