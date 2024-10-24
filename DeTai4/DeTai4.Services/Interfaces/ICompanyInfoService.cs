﻿using System.Collections.Generic;
using System.Threading.Tasks;
using DeTai4.Repositories.Entities;

namespace DeTai4.Services.Interfaces
{
    public interface ICompanyInfoService
    {
        Task<IEnumerable<CompanyInfo>> GetAllCompanyInfosAsync();
        Task<CompanyInfo?> GetCompanyInfoByIdAsync(int companyId);
        Task CreateCompanyInfoAsync(CompanyInfo companyInfo);
        Task UpdateCompanyInfoAsync(CompanyInfo companyInfo);
        Task DeleteCompanyInfoAsync(int companyId);
    }
}
