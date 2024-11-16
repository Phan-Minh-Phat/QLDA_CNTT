using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DeTai4.Pages.Customer
{
    public class SubmitFeedbackModel : PageModel
    {
        [BindProperty]
        public required FeedbackViewModel Feedback { get; set; }

        public void OnGet()
        {
            // Initialize any required values or settings here
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // If validation fails, redisplay the form
                return Page();
            }

            // Process feedback (e.g., save to database, send an email, etc.)

            TempData["SuccessMessage"] = "C?m ?n b?n ?� g?i ph?n h?i!";
            return RedirectToPage("FeedbackSuccess"); // Redirect to a success page
        }
    }

    public class FeedbackViewModel
    {
        [Required(ErrorMessage = "Vui l�ng nh?p t�n c?a b?n.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Vui l�ng nh?p email c?a b?n.")]
        [EmailAddress(ErrorMessage = "??a ch? email kh�ng h?p l?.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Vui l�ng ?�nh gi� (1-5).")]
        [Range(1, 5, ErrorMessage = "?�nh gi� ph?i t? 1 ??n 5.")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Vui l�ng nh?p ph?n h?i c?a b?n.")]
        public required string Comments { get; set; }
    }
}