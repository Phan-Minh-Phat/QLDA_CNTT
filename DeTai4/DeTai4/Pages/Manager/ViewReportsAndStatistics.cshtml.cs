using Microsoft.AspNetCore.Mvc.RazorPages;
using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;

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
            // Lấy tất cả các báo cáo từ dịch vụ
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

            TempData["SuccessMessage"] = "Báo cáo mới đã được thêm thành công!";
            return RedirectToPage();
        }
    }
}
