using System;
using System.Collections.Generic;

namespace DeTai4.Repositories.Entities;

public partial class CompanyInfo
{
    public int CompanyId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }
}
