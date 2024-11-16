using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Web.Pages
{
    public class ManageStaffModel : PageModel
    {
        private readonly IStaffService _staffService;

        public ManageStaffModel(IStaffService staffService)
        {
            _staffService = staffService;
        }

        public IEnumerable<Staff> StaffList { get; set; }

        public async Task OnGetAsync()
        {
            // Fetch all staff
            StaffList = await _staffService.GetAllStaffAsync();
        }

        // Handler for adding new staff
        public async Task<IActionResult> OnPostAddStaffAsync(string fullName, string role)
        {
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(role))
            {
                ModelState.AddModelError("", "Full Name and Role are required.");
                return Page();
            }

            // Create new staff instance
            var newStaff = new Staff
            {
                FullName = fullName,
                Role = role
            };

            // Add staff using service
            await _staffService.AddStaffAsync(newStaff);

            // Redirect to the same page to reload staff list
            return RedirectToPage();
        }

        // You can add OnPost methods for delete/edit actions here.
    }
}
