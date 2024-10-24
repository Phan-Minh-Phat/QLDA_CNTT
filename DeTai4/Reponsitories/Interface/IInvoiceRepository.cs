using System.Collections.Generic;
using System.Threading.Tasks;
using DeTai4.Repositories.Entities;

namespace DeTai4.Repositories
{
    public interface IInvoiceRepository
    {
        Task<IEnumerable<Invoice>> GetAllInvoicesAsync();
        Task<Invoice?> GetInvoiceByIdAsync(int invoiceId);
        Task AddInvoiceAsync(Invoice invoice);
        Task UpdateInvoiceAsync(Invoice invoice);
        Task DeleteInvoiceAsync(int invoiceId);
    }
}
