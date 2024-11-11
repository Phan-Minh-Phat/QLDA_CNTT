using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace DeTai4.Pages.Consulting_Staff
{
    public class TrackProjectModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string ProjectId { get; set; }

        public string ProjectStatus { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime ExpectedCompletionDate { get; set; }

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(ProjectId))
            {
                // Simulate fetching project data
                FetchProjectDetails(ProjectId);
            }
        }

        private void FetchProjectDetails(string projectId)
        {
            // Replace with actual project fetching logic
            ProjectStatus = "?ang thi c�ng";
            ProjectDescription = "D? �n x�y d?ng h? c� Koi cho kh�ch h�ng ABC.";
            ExpectedCompletionDate = DateTime.Today.AddDays(30);

            System.Console.WriteLine($"Fetched details for Project {projectId}");
        }
    }
}
