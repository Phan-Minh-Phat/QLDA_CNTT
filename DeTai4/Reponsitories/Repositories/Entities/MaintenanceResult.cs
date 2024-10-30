using System;
using System.Collections.Generic;

namespace DeTai4.Reponsitories.Repositories.Entities;

public partial class MaintenanceResult
{
    public int MaintenanceResultId { get; set; }

    public int? ServiceId { get; set; }

    public int? StaffId { get; set; }

    public string? ResultDescription { get; set; }

    public DateTime? ResultDate { get; set; }

    public virtual Service? Service { get; set; }

    public virtual Staff? Staff { get; set; }
}
