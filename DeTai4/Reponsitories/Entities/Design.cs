using System;
using System.Collections.Generic;

namespace DeTai4.Repositories.Entities;

public partial class Design
{
    public int DesignId { get; set; }

    public string DesignName { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public decimal Cost { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
