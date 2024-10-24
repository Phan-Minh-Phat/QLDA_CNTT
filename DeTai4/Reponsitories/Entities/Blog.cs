using System;
using System.Collections.Generic;

namespace DeTai4.Repositories.Entities;

public partial class Blog
{
    public int PostId { get; set; }

    public string Title { get; set; } = null!;

    public string? Summary { get; set; }

    public string? Content { get; set; }

    public DateTime? CreatedDate { get; set; }
}
