using Microsoft.AspNetCore.Mvc.RazorPages;
using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Services.Interfaces;
using System.Threading.Tasks;

public class CompanyInfoModel : PageModel
{
    private readonly ICompanyInfoService _companyInfoService;

    public CompanyInfoModel(ICompanyInfoService companyInfoService)
    {
        _companyInfoService = companyInfoService;
    }

    public CompanyInfo? Company { get; private set; }

    public async Task OnGetAsync()
    {
        // Giả sử bạn chỉ có 1 thông tin công ty, lấy thông tin đầu tiên
        Company = await _companyInfoService.GetCompanyInfoByIdAsync(1);
    }
}
