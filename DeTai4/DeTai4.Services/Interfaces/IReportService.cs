using DeTai4.Reponsitories.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Services.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<Report>> GetAllReportsAsync();
        Task<Report?> GetReportByIdAsync(int reportId);
        Task CreateReportAsync(Report report);
        Task UpdateReportAsync(Report report);
        Task DeleteReportAsync(int reportId);
        Task<Report?> GetReportByDateAsync(DateTime reportDate);
        byte[] ExportReportsToExcel(IEnumerable<Report> reports);
    }
}
