using System.Collections.Generic;
using System.Threading.Tasks;
using DeTai4.Reponsitories.Repositories.Entities;
namespace DeTai4.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<Project?> GetProjectByIdAsync(int projectId);
        Task AddProjectAsync(Project project);
        Task UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(int projectId);
        Task<IEnumerable<Project>> GetPendingProjectsAsync();
        Task<IEnumerable<Project>> GetPendingProjectsForStaffAsync(int staffId);
        Task<IEnumerable<Project>> GetProjectsByCustomerIdAsync(int customerId);
        Task<List<Project>> GetProjectsWithConstructionStaffAsync();
        Task<IEnumerable<Project>> GetProjectsForStaffAsync(int staffId);
        Task<IEnumerable<Project>> GetCompletedProjectsAsync();
        Task AddMaintenanceResultAsync(MaintenanceResult maintenanceResult);
    }
}
