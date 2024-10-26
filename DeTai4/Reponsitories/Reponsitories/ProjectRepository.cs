using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeTai4.Repositories.Implementations
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DeTai4Context _context;

        public ProjectRepository(DeTai4Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _context.Projects.Include(p => p.Customer).ToListAsync();
        }

        public async Task<Project?> GetProjectByIdAsync(int projectId)
        {
            return await _context.Projects.FindAsync(projectId);
        }

        public async Task AddProjectAsync(Project project)
        {
             _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            var project = await GetProjectByIdAsync(projectId);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Project>> GetPendingProjectsAsync()
        {
            return await _context.Projects
                                 .Where(p => p.Status == "Pending")
                                 .Include(p => p.Customer)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetPendingProjectsForStaffAsync(int staffId)
        {
            return await _context.Projects
                                 .Include(p => p.Customer)  // Bao gồm Customer để lấy thông tin khách hàng
                                 .ThenInclude(c => c.User)  // Bao gồm User từ Customer
                                 .Where(p => p.StaffId == staffId && p.Status == "Pending")
                                 .ToListAsync();
        }
    }
}
