using Microsoft.AspNetCore.Mvc.RazorPages;
using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using DeTai4.Services;

namespace DeTai4.Pages.Consulting_Staff
{
    public class CreateQuotationModel : PageModel
    {
        private readonly IProjectService _projectService;
        private readonly IQuotationService _quotationService;

        public CreateQuotationModel(IProjectService projectService, IQuotationService quotationService)
        {
            _projectService = projectService;
            _quotationService = quotationService;
        }

        public List<Project> PendingProjects { get; set; } = new List<Project>();

        [BindProperty]
        public int ProjectId { get; set; }

        [BindProperty]
        public decimal QuotationAmount { get; set; }

        [BindProperty]
        public string QuotationDetails { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Lấy danh sách các dự án có trạng thái "Pending"
            PendingProjects = (await _projectService.GetPendingProjectsAsync()).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostCreateQuotationAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Tạo báo giá mới cho dự án
            var quotation = new Quotation
            {
                ProjectId = ProjectId,
                Amount = QuotationAmount,
                Details = QuotationDetails,
                CreatedDate = DateTime.Now
            };

            await _quotationService.CreateQuotationAsync(quotation);

            // Cập nhật trạng thái của dự án thành "Quoted" sau khi lập báo giá
            var project = await _projectService.GetProjectByIdAsync(ProjectId);
            if (project != null)
            {
                project.Status = "Quoted";
                project.QuotationAmount = QuotationAmount;  // Lưu số tiền báo giá vào cột QuotationAmount
                project.QuotationDetails = QuotationDetails; // Lưu chi tiết báo giá vào cột QuotationDetails
                project.QuotationDate = DateTime.Now;
                await _projectService.UpdateProjectAsync(project);
            }

            return RedirectToPage("/Consulting_Staff/CreateQuotation");
        }
    }
}
