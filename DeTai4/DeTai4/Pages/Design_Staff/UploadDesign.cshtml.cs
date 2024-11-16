using DeTai4.Services.Interfaces;
using DeTai4.Reponsitories.Repositories.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Threading.Tasks;

namespace DeTai4.Pages.Design_Staff
{
    public class UploadDesignModel : PageModel
    {
        private readonly IDesignService _designService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public Design NewDesign { get; set; }

        [BindProperty]
        public IFormFile? ImageFile { get; set; }  // Thêm d?u ? ?? ImageFile có th? nh?n giá tr? null

        // Constructor ?? inject các service
        public UploadDesignModel(IDesignService designService, IWebHostEnvironment webHostEnvironment)
        {
            _designService = designService;
            _webHostEnvironment = webHostEnvironment;
            NewDesign = new Design();
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    // X? lý t?i lên ?nh
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    string fileName = Path.GetFileName(ImageFile.FileName);
                    string filePath = Path.Combine(uploadDir, fileName);

                    // L?u ?nh vào th? m?c images
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(fileStream);
                    }

                    // Gán URL hình ?nh cho thi?t k?
                    NewDesign.ImageUrl = $"/images/{fileName}";
                }

                try
                {
                    // L?u thi?t k? m?i vào h? th?ng
                    await _designService.AddDesignAsync(NewDesign);
                    return RedirectToPage("/Design_Staff/ManageDesigns");
                }
                catch (Exception ex)
                {
                    ViewData["ErrorMessage"] = $"?ã có l?i x?y ra: {ex.Message}";
                    return Page();
                }
            }

            return Page();
        }
    }
}

