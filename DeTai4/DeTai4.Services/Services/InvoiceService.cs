using System.Collections.Generic;
using System.Threading.Tasks;
using DeTai4.Repositories;
using DeTai4.Reponsitories.Repositories.Entities;

namespace DeTai4.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            return await _invoiceRepository.GetAllInvoicesAsync();
        }

        public async Task<Invoice?> GetInvoiceByIdAsync(int invoiceId)
        {
            return await _invoiceRepository.GetInvoiceByIdAsync(invoiceId);
        }

        public async Task CreateInvoiceAsync(Invoice invoice)
        {
            // Business logic for creating an invoice can be added here
            await _invoiceRepository.AddInvoiceAsync(invoice);
        }

        public async Task UpdateInvoiceAsync(Invoice invoice)
        {
            // Business logic for updating an invoice can be added here
            await _invoiceRepository.UpdateInvoiceAsync(invoice);
        }

        public async Task DeleteInvoiceAsync(int invoiceId)
        {
            // Business logic for deleting an invoice can be added here
            await _invoiceRepository.DeleteInvoiceAsync(invoiceId);
        }
    }
}
