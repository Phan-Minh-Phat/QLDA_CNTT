using System;
using System.Collections.Generic;

namespace DeTai4.Repositories.Entities;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int? OrderId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? FeedbackDate { get; set; }

    public virtual Order? Order { get; set; }
}
