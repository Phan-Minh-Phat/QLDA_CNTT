using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace DeTai4.Pages.Customer
{
    public class TrackProjectProgressModel : PageModel
    {
        public required List<ProjectViewModel> Projects { get; set; }

        public void OnGet()
        {
            // Example of dummy data for illustration. Replace with real data.
            Projects = new List<ProjectViewModel>
            {
                new ProjectViewModel
                {
                    ProjectId = 1,
                    ProjectName = "D? Án 1",
                    StartDate = DateTime.Now.AddMonths(-1),
                    EndDate = DateTime.Now.AddMonths(1),
                    ProgressPercent = 50,
                    Status = "?ang Th?c Hi?n"
                },
                new ProjectViewModel
                {
                    ProjectId = 2,
                    ProjectName = "D? Án 2",
                    StartDate = DateTime.Now.AddMonths(-2),
                    EndDate = DateTime.Now,
                    ProgressPercent = 100,
                    Status = "Hoàn Thành"
                }
            };
        }
    }

    public class ProjectViewModel
    {
        public int ProjectId { get; set; }
        public required string ProjectName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ProgressPercent { get; set; }
        public required string Status { get; set; }
    }
}