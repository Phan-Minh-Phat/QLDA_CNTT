using System.Collections.Generic;
using System.Threading.Tasks;
using DeTai4.Repositories;
using DeTai4.Repositories.Entities;

namespace DeTai4.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _projectRepository.GetAllProjectsAsync();
        }

        public async Task<Project?> GetProjectByIdAsync(int projectId)
        {
            return await _projectRepository.GetProjectByIdAsync(projectId);
        }

        public async Task CreateProjectAsync(Project project)
        {
            // Business logic for creating a project can be added here
            await _projectRepository.AddProjectAsync(project);
        }

        public async Task UpdateProjectAsync(Project project)
        {
            // Business logic for updating a project can be added here
            await _projectRepository.UpdateProjectAsync(project);
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            // Business logic for deleting a project can be added here
            await _projectRepository.DeleteProjectAsync(projectId);
        }
    }
}
