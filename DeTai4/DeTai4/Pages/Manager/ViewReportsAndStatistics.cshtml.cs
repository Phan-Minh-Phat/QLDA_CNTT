using Microsoft.AspNetCore.Mvc.RazorPages;
using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Composition;

namespace DeTai4.Pages.Manager
{
    public class ViewReportsAndStatisticsModel : PageModel
    {
        private readonly IReportService _reportService;

        public ViewReportsAndStatisticsModel(IReportService reportService)
        {
            _reportService = reportService;
        }
        public List<Report> Reports { get; set; } = new List<Report>();

        [BindProperty]
        public int TotalProjects { get; set; }

        [BindProperty]
        public int CompletedProjects { get; set; }

        [BindProperty]
        public decimal TotalQuotationAmount { get; set; }

        [BindProperty]
        public int ActivePromotions { get; set; }

        public async Task OnGetAsync()
        {
            // L?y t?t c? các báo cáo t? d?ch v?
            Reports = (await _reportService.GetAllReportsAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAddReportAsync()
        {
            var newReport = new Report
            {
                ReportDate = DateTime.Now,
                TotalProjects = TotalProjects,
                CompletedProjects = CompletedProjects,
                TotalQuotationAmount = TotalQuotationAmount,
                ActivePromotions = ActivePromotions
            };

            await _reportService.CreateReportAsync(newReport);

            TempData["SuccessMessage"] = "Báo cáo m?i ?ã ???c thêm thành công!";
            return RedirectToPage();
        }
    }
}
