using System.Collections.Generic;
using System.Threading.Tasks;
using DeTai4.Repositories.Entities;

namespace DeTai4.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<Project?> GetProjectByIdAsync(int projectId);
        Task CreateProjectAsync(Project project);
        Task UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(int projectId);
    }
}
