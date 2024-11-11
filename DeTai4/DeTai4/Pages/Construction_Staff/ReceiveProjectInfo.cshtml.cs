using System.Threading.Tasks;
using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Services;
using DeTai4.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeTai4.Pages.Construction_Staff
{
    public class ReceiveProjectInfoModel : PageModel
    {
        private readonly IProjectService _projectService;

        [BindProperty]
        public Project Project { get; set; }

        public ReceiveProjectInfoModel(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public void OnGet()
        {
            // This method can be used to load any initial data if needed.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _projectService.AddProjectAsync(Project);
            return RedirectToPage("/Construction_Staff/ReceiveProjectInfo");
        }
    }
}
