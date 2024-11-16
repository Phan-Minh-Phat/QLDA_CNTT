using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Services;
using DeTai4.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Pages
{
    public class ManageProjectsModel : PageModel
    {
        private readonly IProjectService _projectService;

        public ManageProjectsModel(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public IEnumerable<Project> Projects { get; private set; }

        public async Task OnGetAsync()
        {
            Projects = await _projectService.GetAllProjectsAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _projectService.DeleteProjectAsync(id);
            return RedirectToPage();
        }
    }
}
