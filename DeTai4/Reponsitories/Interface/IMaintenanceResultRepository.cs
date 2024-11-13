using DeTai4.Reponsitories.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeTai4.Reponsitories.Interface
{
    public interface IMaintenanceResultRepository
    {
         Task<IEnumerable<MaintenanceResult>> GetAllMaintenanceResultsAsync();
        Task<MaintenanceResult?> GetMaintenanceResultByIdAsync(int maintenanceResultId);
        Task AddMaintenanceResultAsync(MaintenanceResult maintenanceResult);
        Task UpdateMaintenanceResultAsync(MaintenanceResult maintenanceResult);
        Task DeleteMaintenanceResultAsync(int maintenanceResultId);
    }
}
