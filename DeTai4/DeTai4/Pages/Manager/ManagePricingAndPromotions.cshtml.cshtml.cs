using System.Collections.Generic;
using System.Threading.Tasks;
using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeTai4.Pages
{
    public class ManagePricingAndPromotionsModel : PageModel
    {
        private readonly IPromotionService _promotionService;

        public ManagePricingAndPromotionsModel(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        public IEnumerable<Promotion> Promotions { get; private set; }

        public async Task OnGetAsync()
        {
            Promotions = await _promotionService.GetAllPromotionsAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _promotionService.DeletePromotionAsync(id);
            return RedirectToPage();
        }
    }
}
