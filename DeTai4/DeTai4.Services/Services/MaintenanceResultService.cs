using DeTai4.Reponsitories.Interface;
using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Repositories.Implementations;
using DeTai4.Repositories.Interfaces;
using DeTai4.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Services.Implementations
{
    public class MaintenanceResultService : IMaintenanceResultService
    {
        private readonly IMaintenanceResultRepository _maintenanceResultRepository;

        public MaintenanceResultService(IMaintenanceResultRepository maintenanceResultRepository)
        {
            _maintenanceResultRepository = maintenanceResultRepository;
        }

        public async Task<IEnumerable<MaintenanceResult>> GetAllMaintenanceResultsAsync()
        {
            return await _maintenanceResultRepository.GetAllMaintenanceResultsAsync();
        }

        public async Task<MaintenanceResult?> GetMaintenanceResultByIdAsync(int maintenanceResultId)
        {
            return await _maintenanceResultRepository.GetMaintenanceResultByIdAsync(maintenanceResultId);
        }

        public async Task AddMaintenanceResultAsync(MaintenanceResult maintenanceResult)
        {
            await _maintenanceResultRepository.AddMaintenanceResultAsync(maintenanceResult);
        }

        public async Task UpdateMaintenanceResultAsync(MaintenanceResult maintenanceResult)
        {
            await _maintenanceResultRepository.UpdateMaintenanceResultAsync(maintenanceResult);
        }

        public async Task DeleteMaintenanceResultAsync(int maintenanceResultId)
        {
            await _maintenanceResultRepository.DeleteMaintenanceResultAsync(maintenanceResultId);
        }
    }
}
