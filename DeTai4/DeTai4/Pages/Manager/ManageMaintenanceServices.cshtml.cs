using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;

namespace DeTai4.Pages.Manager
{
    public class ManageMaintenanceServicesModel : PageModel
    {
        private readonly IServiceService _serviceService;

        public ManageMaintenanceServicesModel(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        public List<Service> Services { get; set; } = new List<Service>();

        [BindProperty]
        public Service Service { get; set; } = new Service();

        public bool IsEditing => Service.ServiceId != 0;
        public string SubmitButtonText => IsEditing ? "Cập nhật" : "Thêm mới";

        public async Task OnGetAsync()
        {
            Services = (await _serviceService.GetAllServicesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Service.ServiceId == 0)
            {
                await _serviceService.AddServiceAsync(Service);
                TempData["SuccessMessage"] = "Dịch vụ bảo dưỡng đã được thêm thành công!";
            }
            else
            {
                await _serviceService.UpdateServiceAsync(Service);
                TempData["SuccessMessage"] = "Dịch vụ bảo dưỡng đã được cập nhật thành công!";
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetEditAsync(int id)
        {
            Service = await _serviceService.GetServiceByIdAsync(id);
            if (Service == null)
            {
                TempData["ErrorMessage"] = "Dịch vụ không tồn tại.";
                return RedirectToPage();
            }
            await OnGetAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _serviceService.DeleteServiceAsync(id);
            TempData["SuccessMessage"] = "Dịch vụ bảo dưỡng đã được xóa thành công!";
            return RedirectToPage();
        }
    }
}
