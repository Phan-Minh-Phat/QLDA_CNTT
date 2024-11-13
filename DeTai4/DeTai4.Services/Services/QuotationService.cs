using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Repositories.Interfaces;
using DeTai4.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeTai4.Services.Implementations
{
    public class QuotationService : IQuotationService
    {
        private readonly IQuotationRepository _quotationRepository;

        public QuotationService(IQuotationRepository quotationRepository)
        {
            _quotationRepository = quotationRepository;
        }

        public async Task CreateQuotationAsync(Quotation quotation)
        {
            await _quotationRepository.CreateQuotationAsync(quotation);
        }

        public async Task<IEnumerable<Quotation>> GetQuotationsByProjectIdAsync(int projectId)
        {
            return await _quotationRepository.GetQuotationsByProjectIdAsync(projectId);
        }

        public async Task<Quotation?> GetQuotationByIdAsync(int quotationId)
        {
            return await _quotationRepository.GetQuotationByIdAsync(quotationId);
        }

        public async Task UpdateQuotationAsync(Quotation quotation)
        {
            await _quotationRepository.UpdateQuotationAsync(quotation);
        }

        public async Task DeleteQuotationAsync(int quotationId)
        {
            await _quotationRepository.DeleteQuotationAsync(quotationId);
        }

        public async Task<IEnumerable<Quotation>> GetAllQuotationsAsync()
        {
            return await _quotationRepository.GetAllQuotationsAsync();
        }
    }
}
