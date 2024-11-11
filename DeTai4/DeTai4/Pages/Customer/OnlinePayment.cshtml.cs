using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DeTai4.Pages.Customer;

namespace DeTai4.Pages.Customer
{
    public class OnlinePaymentModel : PageModel
    {
        [BindProperty]
        public required OnlinePayment Payment { get; set; }

        public void OnGet()
        {
            // Initialize any required values here, if needed
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // If validation fails, show errors in the form
                return Page();
            }

            // Add payment processing logic here (e.g., payment gateway integration)

            TempData["SuccessMessage"] = "Thanh toán thành công!";
            return RedirectToPage("Success"); // Redirect to a success page after successful payment
        }
    }

    public class OnlinePayment
    {
        public required string CardNumber { get; set; }
        public required string CardHolderName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public required string CVV {  get; set; }
     
        public decimal Amount { get; set; }
    }
}