using System.Collections.Generic;
using System.Threading.Tasks;
using DeTai4.Repositories.Entities;
using DeTai4.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeTai4.Repositories.Implementations
{
    public class CompanyInfoRepository : ICompanyInfoRepository
    {
        private readonly DbContext _context;

        public CompanyInfoRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CompanyInfo>> GetAllCompanyInfosAsync()
        {
            return await _context.Set<CompanyInfo>().ToListAsync();
        }

        public async Task<CompanyInfo?> GetCompanyInfoByIdAsync(int companyId)
        {
            return await _context.Set<CompanyInfo>().FindAsync(companyId);
        }

        public async Task CreateCompanyInfoAsync(CompanyInfo companyInfo)
        {
            await _context.Set<CompanyInfo>().AddAsync(companyInfo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCompanyInfoAsync(CompanyInfo companyInfo)
        {
            _context.Set<CompanyInfo>().Update(companyInfo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCompanyInfoAsync(int companyId)
        {
            var companyInfo = await GetCompanyInfoByIdAsync(companyId);
            if (companyInfo != null)
            {
                _context.Set<CompanyInfo>().Remove(companyInfo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
