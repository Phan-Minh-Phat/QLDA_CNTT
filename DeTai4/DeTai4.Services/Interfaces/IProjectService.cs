using System.Collections.Generic;
using System.Threading.Tasks;
using DeTai4.Reponsitories.Repositories.Entities;

namespace DeTai4.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<Project?> GetProjectByIdAsync(int projectId);
        Task CreateProjectAsync(Project project);
        Task UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(int projectId);
        Task<IEnumerable<Project>> GetPendingProjectsAsync();
        Task<IEnumerable<Project>> GetProjectsByCustomerIdAsync(int customerId);

        Task<IEnumerable<Project>> GetPendingProjectsForStaffAsync(int staffId);
        Task<List<Project>> GetProjectsWithConstructionStaffAsync();
        Task<IEnumerable<Project>> GetProjectsForStaffAsync(int staffId);
        Task<IEnumerable<Project>> GetCompletedProjectsAsync();
        Task ProvideMaintenanceAsync(int projectId, int serviceId, int staffId, string resultDescription);


    }
}
