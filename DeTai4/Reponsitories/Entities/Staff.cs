using System;
using System.Collections.Generic;

namespace DeTai4.Repositories.Entities;

public partial class Staff
{
    public int StaffId { get; set; }

    public int UserId { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<MaintenanceResult> MaintenanceResults { get; set; } = new List<MaintenanceResult>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Project> ProjectsNavigation { get; set; } = new List<Project>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
