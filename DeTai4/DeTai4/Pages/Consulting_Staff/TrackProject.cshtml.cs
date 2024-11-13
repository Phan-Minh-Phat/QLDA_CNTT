using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeTai4.Services;

namespace DeTai4.Pages.Consulting_Staff
{
    public class TrackProjectModel : PageModel
    {
        private readonly IProjectService _projectService;

        public TrackProjectModel(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public IEnumerable<Project> PendingProjects { get; set; } = new List<Project>();

        public async Task OnGetAsync()
        {
            PendingProjects = await _projectService.GetPendingProjectsAsync();
        }
    }
}
