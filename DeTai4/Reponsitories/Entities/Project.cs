using System;
using System.Collections.Generic;

namespace DeTai4.Repositories.Entities;

public partial class Project
{
    public int ProjectId { get; set; }

    public int? CustomerId { get; set; }

    public int? DesignId { get; set; }

    public string? ProjectName { get; set; }

    public int? StaffId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Status { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Design? Design { get; set; }

    public virtual Staff? Staff { get; set; }

    public virtual ICollection<Staff> StaffNavigation { get; set; } = new List<Staff>();
}
