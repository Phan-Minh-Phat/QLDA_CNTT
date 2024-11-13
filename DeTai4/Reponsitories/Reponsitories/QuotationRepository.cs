using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DeTai4.Repositories.Implementations
{
    public class QuotationRepository : IQuotationRepository
    {
        private readonly DeTai4Context _context;

        public QuotationRepository(DeTai4Context context)
        {
            _context = context;
        }

        public async Task CreateQuotationAsync(Quotation quotation)
        {
            await _context.Quotations.AddAsync(quotation);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Quotation>> GetQuotationsByProjectIdAsync(int projectId)
        {
            return await _context.Quotations
                .Where(q => q.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<Quotation?> GetQuotationByIdAsync(int quotationId)
        {
            return await _context.Quotations.FindAsync(quotationId);
        }

        public async Task UpdateQuotationAsync(Quotation quotation)
        {
            _context.Quotations.Update(quotation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuotationAsync(int quotationId)
        {
            var quotation = await _context.Quotations.FindAsync(quotationId);
            if (quotation != null)
            {
                _context.Quotations.Remove(quotation);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Quotation>> GetAllQuotationsAsync()
        {
            return await _context.Quotations.Include(q => q.Project).ToListAsync();
        }
    }
}
