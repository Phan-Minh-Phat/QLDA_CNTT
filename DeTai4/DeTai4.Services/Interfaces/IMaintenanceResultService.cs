using DeTai4.Reponsitories.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Services.Interfaces
{
    public interface IMaintenanceResultService
    {
        Task<IEnumerable<MaintenanceResult>> GetAllMaintenanceResultsAsync();
        Task<MaintenanceResult?> GetMaintenanceResultByIdAsync(int maintenanceResultId);
        Task AddMaintenanceResultAsync(MaintenanceResult maintenanceResult);
        Task UpdateMaintenanceResultAsync(MaintenanceResult maintenanceResult);
        Task DeleteMaintenanceResultAsync(int maintenanceResultId);
    }
}
