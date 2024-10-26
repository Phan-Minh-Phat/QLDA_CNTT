using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DesignSamplesModel : PageModel
{
    private readonly IDesignService _designService;

    public DesignSamplesModel(IDesignService designService)
    {
        _designService = designService;
    }

    public List<Design> Designs { get; set; } = new();

    public async Task OnGetAsync()
    {
        var designs = await _designService.GetAllDesignsAsync();
        Designs = new List<Design>(designs);
    }
}
