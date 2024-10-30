using DeTai4.Repositories.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeTai4.Repositories;

namespace DeTai4.Services.Implementations
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
            if (project == null) throw new ArgumentNullException(nameof(project));

            await _projectRepository.AddProjectAsync(project);
        }

        public async Task UpdateProjectAsync(Project project)
        {
            await _projectRepository.UpdateProjectAsync(project);
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            await _projectRepository.DeleteProjectAsync(projectId);
        }

        public async Task<IEnumerable<Project>> GetPendingProjectsAsync()
        {
            return await _projectRepository.GetPendingProjectsAsync();
        }

        public async Task<IEnumerable<Project>> GetPendingProjectsForStaffAsync(int staffId)
        {
            return await _projectRepository.GetPendingProjectsAsync();
        }
    }
}
