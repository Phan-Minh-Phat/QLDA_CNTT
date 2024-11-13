using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using System.Threading.Tasks;

namespace DeTai4.Pages.Design_Staff
{
    public class EditDesignModel : PageModel
    {
        private readonly IDesignService _designService;

        public EditDesignModel(IDesignService designService)
        {
            _designService = designService;
        }

        [BindProperty]
        public Design Design { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Lấy thông tin mẫu thiết kế theo ID
            Design = await _designService.GetDesignByIdAsync(id);

            if (Design == null)
            {
                return RedirectToPage("/NotFound"); // Chuyển hướng nếu không tìm thấy thiết kế
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Cập nhật thông tin mẫu thiết kế trong cơ sở dữ liệu
            await _designService.UpdateDesignAsync(Design);

            // Chuyển hướng về trang quản lý sau khi cập nhật thành công
            return RedirectToPage("/Design_Staff/ManageDesigns");
        }
    }
}
