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
            

        public async Task<IEnumerable<Project>> GetProjectsByCustomerIdAsync(int customerId)
        {
            return await _projectRepository.GetProjectsByCustomerIdAsync(customerId);
        }

        public async Task<List<Project>> GetProjectsWithConstructionStaffAsync()
        {
            return await _projectRepository.GetProjectsWithConstructionStaffAsync();
        }
        public async Task<IEnumerable<Project>> GetProjectsForStaffAsync(int staffId)
        {
            return await _projectRepository.GetProjectsForStaffAsync(staffId);
        }

        public async Task<IEnumerable<Project>> GetCompletedProjectsAsync()
        {
            return await _projectRepository.GetCompletedProjectsAsync();
        }

        public async Task ProvideMaintenanceAsync(int projectId, int serviceId, int staffId, string resultDescription)
        {
            var maintenanceResult = new MaintenanceResult
            {
                ProjectId = projectId,
                ServiceId = serviceId,
                StaffId = staffId,
                ResultDescription = resultDescription,
                ResultDate = DateTime.Now
            };

            await _projectRepository.AddMaintenanceResultAsync(maintenanceResult);
        }
    }
}
