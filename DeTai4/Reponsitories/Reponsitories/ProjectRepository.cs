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
            return await _context.Projects
                                   .Include(p => p.Customer) // Bao gồm thông tin cần thiết nếu có
                                   .FirstOrDefaultAsync(p => p.ProjectId == projectId);
        }

        public async Task AddProjectAsync(Project project)
        {
             _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(Project project)
        {
            _context.Projects.Attach(project); // Đảm bảo đối tượng đang được theo dõi bởi DbContext
            _context.Entry(project).Property(p => p.Status).IsModified = true; // Đánh dấu cột Status là đã thay đổi
            _context.Entry(project).Property(p => p.EndDate).IsModified = true; // Đánh dấu EndDate là đã thay đổi
            _context.Entry(project).Property(p => p.RequestDetails).IsModified = true; // Đánh dấu RequestDetails nếu cần
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
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

        public async Task<IEnumerable<Project>> GetProjectsByCustomerIdAsync(int customerId)
        {
            return await _context.Projects
                                  .Where(p => p.CustomerId == customerId)
                                  .Include(p => p.Customer)
                                  .ToListAsync();
        }

        public async Task<List<Project>> GetProjectsWithConstructionStaffAsync()
        {
            return await _context.Projects
                .Include(p => p.Staff) // Bao gồm thông tin nhân viên thi công nếu có
                .Where(p => p.Status == "In Progress" && p.StaffId != null) // Lọc dự án đang tiến hành và có nhân viên thi công
                .ToListAsync();
        }
        public async Task<IEnumerable<Project>> GetProjectsForStaffAsync(int staffId)
        {
            return await _context.Projects
                         .Where(p => p.StaffId == staffId)
                         .Include(p => p.Customer)  // Nếu cần bao gồm thông tin khách hàng
                         .ToListAsync(); 
        }
        public async Task<IEnumerable<Project>> GetCompletedProjectsAsync()
        {
            return await _context.Projects
                                 .Where(p => p.Status == "Completed")
                                 .Include(p => p.Customer)
                                 .ToListAsync();
        }

        public async Task AddMaintenanceResultAsync(MaintenanceResult maintenanceResult)
        {
            await _context.MaintenanceResults.AddAsync(maintenanceResult);
            await _context.SaveChangesAsync();
        }
    }
}
