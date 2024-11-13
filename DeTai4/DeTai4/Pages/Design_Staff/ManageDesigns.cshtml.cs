using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Pages.Design_Staff
{
    public class ManageDesignsModel : PageModel
    {
        private readonly IDesignService _designService;

        public ManageDesignsModel(IDesignService designService)
        {
            _designService = designService;
        }

        public List<Design> Designs { get; set; } = new List<Design>();

        public async Task OnGetAsync()
        {
            // Lấy danh sách tất cả các mẫu thiết kế từ cơ sở dữ liệu
            Designs = (await _designService.GetAllDesignsAsync()).ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int designId)
        {
            // Xóa mẫu thiết kế theo ID
            await _designService.DeleteDesignAsync(designId);
            return RedirectToPage(); // Refresh lại trang sau khi xóa
        }
    }
}
