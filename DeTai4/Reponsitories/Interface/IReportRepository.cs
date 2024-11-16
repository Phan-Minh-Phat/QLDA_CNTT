using DeTai4.Reponsitories.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task<IEnumerable<Report>> GetAllReportsAsync();
        Task<Report?> GetReportByIdAsync(int reportId);
        Task AddReportAsync(Report report);
        Task UpdateReportAsync(Report report);
        Task DeleteReportAsync(int reportId);
        Task<Report?> GetReportByDateAsync(DateTime reportDate); // Tìm báo cáo theo ngày nếu cần
    }
}
