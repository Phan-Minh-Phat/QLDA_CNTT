using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DeTai4.Pages.Customer
{
    public class BookMaintenanceServiceModel : PageModel
    {
        public required List<BookViewModel> Books { get; set; }

        public void OnGet()
        {
            // Fetch the list of books that need maintenance
            Books = GetBooksForMaintenance();
        }

        public IActionResult OnPostDelete(int id)
        {
            // Logic to delete the book from the database
            DeleteBookById(id);

            // Refresh the page after deletion
            return RedirectToPage();
        }

        private List<BookViewModel> GetBooksForMaintenance()
        {
            // Placeholder: Replace with actual database call
            return new List<BookViewModel>
            {
                new BookViewModel { BookId = 1, Title = "Sách A", Author = "Tác Gi? A", EntryDate = DateTime.Now.AddMonths(-3), Status = "C?n B?o Trì" },
                new BookViewModel { BookId = 2, Title = "Sách B", Author = "Tác Gi? B", EntryDate = DateTime.Now.AddMonths(-1), Status = "T?t" }
            };
        }

        private void DeleteBookById(int id)
        {
            // Placeholder: Logic to delete book from the database
        }
    }

    public class BookViewModel
    {
        public int BookId { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public DateTime EntryDate { get; set; }
        public required string Status { get; set; }
    }
}