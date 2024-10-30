using System;
using System.Collections.Generic;

namespace DeTai4.Reponsitories.Repositories.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }
    public int? UserId { get; set; }
    public string? FullName { get; set; } // Thêm cột FullName

    public int? ServiceId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? Status { get; set; }

    public decimal? TotalCost { get; set; }

    public virtual Customer? Customer { get; set; }
    public virtual User? User { get; set; }
    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual Service? Service { get; set; }

}
