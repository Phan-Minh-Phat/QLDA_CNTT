using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeTai4.Repositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeTai4.Repositories
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
            return await _context.Projects
                .Include(p => p.Customer)
                .Include(p => p.Design)
                .Include(p => p.Staff)
                .Include(p => p.StaffNavigation)
                .ToListAsync();
        }

        public async Task<Project?> GetProjectByIdAsync(int projectId)
        {
            return await _context.Projects
                .Include(p => p.Customer)
                .Include(p => p.Design)
                .Include(p => p.Staff)
                .Include(p => p.StaffNavigation)
                .FirstOrDefaultAsync(p => p.ProjectId == projectId);
        }

        public async Task AddProjectAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
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
    }
}
