using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Pages.Design_Staff
{
    public class ManageDesignsModel : PageModel
    {
        private readonly IDesignService _designService;

        // Kh?i t?o Designs v?i giá tr? m?c ??nh là m?t danh sách r?ng
        public IEnumerable<Design> Designs { get; set; } = new List<Design>();

        // Constructor ?? tiêm service
        public ManageDesignsModel(IDesignService designService)
        {
            _designService = designService;
        }

        // L?y t?t c? các thi?t k? t? service
        public async Task<IActionResult> OnGetAsync()
        {
            // L?y danh sách thi?t k? t? service
            Designs = await _designService.GetAllDesignsAsync();

            return Page();
        }
    }
}

