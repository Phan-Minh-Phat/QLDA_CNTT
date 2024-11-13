using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Services;

namespace DeTai4.Pages.Manager
{
    public class ManagePricingAndPromotionsModel : PageModel
    {
        private readonly IQuotationService _quotationService;
        private readonly IPromotionService _promotionService;
        private readonly IProjectService _projectService;

        public ManagePricingAndPromotionsModel(IQuotationService quotationService, IPromotionService promotionService, IProjectService projectService)
        {
            _quotationService = quotationService;
            _promotionService = promotionService;
            _projectService = projectService;
        }

        public List<Quotation> Quotations { get; set; } = new List<Quotation>();
        public List<Promotion> Promotions { get; set; } = new List<Promotion>();
        public List<Project> Projects { get; set; } = new List<Project>();

        [BindProperty]
        public Quotation Quotation { get; set; } = new Quotation();

        [BindProperty]
        public Promotion Promotion { get; set; } = new Promotion();

        public bool IsEditingQuotation => Quotation.QuotationId != 0;
        public bool IsEditingPromotion => Promotion.PromotionId != 0;
        public string QuotationSubmitButtonText => IsEditingQuotation ? "Cập nhật" : "Thêm mới";
        public string PromotionSubmitButtonText => IsEditingPromotion ? "Cập nhật" : "Thêm mới";

        public async Task OnGetAsync()
        {
            Quotations = (await _quotationService.GetAllQuotationsAsync()).ToList();
            Promotions = (await _promotionService.GetAllPromotionsAsync()).ToList();
            Projects = (await _projectService.GetAllProjectsAsync()).ToList();
        }

        public async Task<IActionResult> OnPostSaveQuotationAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Quotation.QuotationId == 0)
            {
                await _quotationService.CreateQuotationAsync(Quotation);
                TempData["SuccessMessage"] = "Bảng giá đã được thêm thành công!";
            }
            else
            {
                await _quotationService.UpdateQuotationAsync(Quotation);
                TempData["SuccessMessage"] = "Bảng giá đã được cập nhật thành công!";
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetEditQuotationAsync(int id)
        {
            Quotation = await _quotationService.GetQuotationByIdAsync(id);
            await OnGetAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteQuotationAsync(int id)
        {
            await _quotationService.DeleteQuotationAsync(id);
            TempData["SuccessMessage"] = "Bảng giá đã được xóa thành công!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSavePromotionAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Promotion.PromotionId == 0)
            {
                await _promotionService.CreatePromotionAsync(Promotion);
                TempData["SuccessMessage"] = "Khuyến mãi đã được thêm thành công!";
            }
            else
            {
                await _promotionService.UpdatePromotionAsync(Promotion);
                TempData["SuccessMessage"] = "Khuyến mãi đã được cập nhật thành công!";
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetEditPromotionAsync(int id)
        {
            Promotion = await _promotionService.GetPromotionByIdAsync(id);
            await OnGetAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeletePromotionAsync(int id)
        {
            await _promotionService.DeletePromotionAsync(id);
            TempData["SuccessMessage"] = "Khuyến mãi đã được xóa thành công!";
            return RedirectToPage();
        }
    }
}
