using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Services;
using DeTai4.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeTai4.Pages.Construction_Staff
{
    public class ManageConstructionProgressModel : PageModel
    {
        private readonly IProjectService _projectService;

        public IEnumerable<ProjectViewModel> Projects { get; set; }

        public ManageConstructionProgressModel(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task OnGetAsync()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            Projects = projects.Select(p => new ProjectViewModel
            {
                ProjectId = p.ProjectId, // Thêm ProjectId vào ViewModel
                ProjectName = p.ProjectName,
                StartDate = p.StartDate.ToString("dd/MM/yyyy"),
                EndDate = p.EndDate.HasValue ? p.EndDate.Value.ToString("dd/MM/yyyy") : "N/A"
            });
        }

        public class ProjectViewModel
        {
            public int ProjectId { get; set; } // Thêm thu?c tính ProjectId
            public string ProjectName { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
        }
    }
}
