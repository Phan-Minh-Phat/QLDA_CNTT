using System;
using System.Collections.Generic;

namespace DeTai4.Repositories.Entities;

public partial class Promotion
{
    public int PromotionId { get; set; }

    public string? PromotionName { get; set; }

    public decimal? Discount { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
