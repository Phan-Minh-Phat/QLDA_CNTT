using DeTai4.Reponsitories.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Services.Interfaces
{
    public interface IQuotationService
    {
        Task<IEnumerable<Quotation>> GetAllQuotationsAsync();

        Task CreateQuotationAsync(Quotation quotation);
        Task<IEnumerable<Quotation>> GetQuotationsByProjectIdAsync(int projectId);
        Task<Quotation?> GetQuotationByIdAsync(int quotationId);
        Task UpdateQuotationAsync(Quotation quotation);
        Task DeleteQuotationAsync(int quotationId);
    }
}
