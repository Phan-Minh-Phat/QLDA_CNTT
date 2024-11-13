using Microsoft.AspNetCore.Mvc.RazorPages;
using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DeTai4.Services;

namespace DeTai4.Pages.Home
{
    public class TrackProjectProgressModel : PageModel
    {
        private readonly IProjectService _projectService;

        public TrackProjectProgressModel(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public List<Project> Projects { get; set; } = new List<Project>();

        public async Task OnGetAsync()
        {
            var customerId = int.Parse(User.FindFirstValue("UserId"));
            var projects = await _projectService.GetProjectsByCustomerIdAsync(customerId);
            Projects = projects != null ? new List<Project>(projects) : new List<Project>();
        }
    }
}   