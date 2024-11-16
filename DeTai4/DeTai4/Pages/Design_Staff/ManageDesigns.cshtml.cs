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

        // Kh?i t?o Designs v?i gi� tr? m?c ??nh l� m?t danh s�ch r?ng
        public IEnumerable<Design> Designs { get; set; } = new List<Design>();

        // Constructor ?? ti�m service
        public ManageDesignsModel(IDesignService designService)
        {
            _designService = designService;
        }

        // L?y t?t c? c�c thi?t k? t? service
        public async Task<IActionResult> OnGetAsync()
        {
            // L?y danh s�ch thi?t k? t? service
            Designs = await _designService.GetAllDesignsAsync();

            return Page();
        }
    }
}

