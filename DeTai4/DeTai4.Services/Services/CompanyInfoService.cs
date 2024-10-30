using System.Collections.Generic;
using System.Threading.Tasks;
using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Repositories.Interfaces;
using DeTai4.Services.Interfaces;

namespace DeTai4.Services.Implementations
{
    public class CompanyInfoService : ICompanyInfoService
    {
        private readonly ICompanyInfoRepository _companyInfoRepository;

        public CompanyInfoService(ICompanyInfoRepository companyInfoRepository)
        {
            _companyInfoRepository = companyInfoRepository;
        }

        public async Task<IEnumerable<CompanyInfo>> GetAllCompanyInfosAsync()
        {
            return await _companyInfoRepository.GetAllCompanyInfosAsync();
        }

        public async Task<CompanyInfo?> GetCompanyInfoByIdAsync(int companyId)
        {
            return await _companyInfoRepository.GetCompanyInfoByIdAsync(companyId);
        }

        public async Task CreateCompanyInfoAsync(CompanyInfo companyInfo)
        {
            // Perform any additional business logic or validation before creating the CompanyInfo
            await _companyInfoRepository.CreateCompanyInfoAsync(companyInfo);
        }

        public async Task UpdateCompanyInfoAsync(CompanyInfo companyInfo)
        {
            // Perform any additional business logic or validation before updating the CompanyInfo
            await _companyInfoRepository.UpdateCompanyInfoAsync(companyInfo);
        }

        public async Task DeleteCompanyInfoAsync(int companyId)
        {
            // Perform any additional business logic or validation before deleting the CompanyInfo
            await _companyInfoRepository.DeleteCompanyInfoAsync(companyId);
        }
    }
}
