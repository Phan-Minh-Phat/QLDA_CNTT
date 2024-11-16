using System;

namespace DeTai4.Reponsitories.Repositories.Entities
{
    public class Report
    {
        public int ReportId { get; set; }
        public DateTime ReportDate { get; set; } // Ngày lập báo cáo
        public int TotalProjects { get; set; }
        public int CompletedProjects { get; set; }
        public decimal TotalQuotationAmount { get; set; }
        public int ActivePromotions { get; set; }
    }
}
