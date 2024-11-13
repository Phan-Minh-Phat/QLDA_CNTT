using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeTai4.Reponsitories.Repositories.Entities
{
    public class Quotation
    {
        public int QuotationId { get; set; }
        public int ProjectId { get; set; }
        public decimal Amount { get; set; }
        public string Details { get; set; }
        public DateTime CreatedDate { get; set; }

        // Liên kết với Project
        public virtual Project Project { get; set; }
    }
}
