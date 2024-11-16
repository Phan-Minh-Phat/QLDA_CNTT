using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Pages
{
    public class ManageMaintenanceServicesModel : PageModel
    {
        private readonly IServiceService _serviceService;

        public ManageMaintenanceServicesModel(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        public IEnumerable<Service> Services { get; private set; }

        public async Task OnGetAsync()
        {
            Services = await _serviceService.GetAllServicesAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _serviceService.DeleteServiceAsync(id);
            return RedirectToPage();
        }
    }
}
