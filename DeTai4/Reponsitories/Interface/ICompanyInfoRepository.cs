using DeTai4.Reponsitories.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace DeTai4.Repositories.Interfaces
{
    public interface ICompanyInfoRepository
    {
        Task<IEnumerable<CompanyInfo>> GetAllCompanyInfosAsync();
        Task<CompanyInfo?> GetCompanyInfoByIdAsync(int companyId);
        Task CreateCompanyInfoAsync(CompanyInfo companyInfo);
        Task UpdateCompanyInfoAsync(CompanyInfo companyInfo);
        Task DeleteCompanyInfoAsync(int companyId);
    }
}
