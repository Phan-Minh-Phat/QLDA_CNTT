using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Repositories.Interfaces;
using DeTai4.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace DeTai4.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<IEnumerable<Report>> GetAllReportsAsync()
        {
            return await _reportRepository.GetAllReportsAsync();
        }

        public async Task<Report?> GetReportByIdAsync(int reportId)
        {
            return await _reportRepository.GetReportByIdAsync(reportId);
        }

        public async Task CreateReportAsync(Report report)
        {
            await _reportRepository.AddReportAsync(report);
        }

        public async Task UpdateReportAsync(Report report)
        {
            await _reportRepository.UpdateReportAsync(report);
        }

        public async Task DeleteReportAsync(int reportId)
        {
            await _reportRepository.DeleteReportAsync(reportId);
        }

        public async Task<Report?> GetReportByDateAsync(DateTime reportDate)
        {
            return await _reportRepository.GetReportByDateAsync(reportDate);
        }
        public byte[] ExportReportsToExcel(IEnumerable<Report> reports)
        {
            // Giả định sử dụng một thư viện như EPPlus để tạo file Excel
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Reports");
                worksheet.Cells[1, 1].Value = "Mã Báo Cáo";
                worksheet.Cells[1, 2].Value = "Ngày Báo Cáo";
                worksheet.Cells[1, 3].Value = "Tổng Số Dự Án";
                worksheet.Cells[1, 4].Value = "Dự Án Hoàn Thành";
                worksheet.Cells[1, 5].Value = "Tổng Số Tiền Báo Giá";
                worksheet.Cells[1, 6].Value = "Số Khuyến Mãi Hiện Hành";

                int row = 2;
                foreach (var report in reports)
                {
                    worksheet.Cells[row, 1].Value = report.ReportId;
                    worksheet.Cells[row, 2].Value = report.ReportDate.ToString("dd/MM/yyyy");
                    worksheet.Cells[row, 3].Value = report.TotalProjects;
                    worksheet.Cells[row, 4].Value = report.CompletedProjects;
                    worksheet.Cells[row, 5].Value = report.TotalQuotationAmount;
                    worksheet.Cells[row, 6].Value = report.ActivePromotions;
                    row++;
                }

                return package.GetAsByteArray();
            }
        }
    }
}
