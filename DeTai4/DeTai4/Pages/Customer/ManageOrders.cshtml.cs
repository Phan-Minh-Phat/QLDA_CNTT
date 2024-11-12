using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace DeTai4.Pages.Customer
{
    public class ManageOrdersModel : PageModel
    {
        // List of orders that a customer has placed
        public required List<OrderViewModel> Orders { get; set; }

        public void OnGet()
        {
            // Fetch the orders for the logged-in customer
            Orders = GetOrdersForCustomer();
        }

        public IActionResult OnPostCancel(int orderId)
        {
            // Logic to cancel the order
            CancelOrderById(orderId);

            // Redirect back to the page after canceling the order
            return RedirectToPage();
        }

        private List<OrderViewModel> GetOrdersForCustomer()
        {
            // Placeholder: Replace with logic to fetch orders for the logged-in customer from the database
            return new List<OrderViewModel>
            {
                new OrderViewModel { OrderId = 1, OrderDate = System.DateTime.Now.AddMonths(-1), ProductName = "S?n ph?m A", Quantity = 2, TotalAmount = 50000, Status = "?ang X? Lý" },
                new OrderViewModel { OrderId = 2, OrderDate = System.DateTime.Now.AddDays(-5), ProductName = "S?n ph?m B", Quantity = 1, TotalAmount = 20000, Status = "?ã Giao" }
            };
        }

        private void CancelOrderById(int orderId)
        {
            // Placeholder: Logic to cancel the order in the database
            // For example, update the status of the order in the database to "Canceled"
        }
    }

    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public System.DateTime OrderDate { get; set; }
        public required string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public required string Status { get; set; }
    }
}