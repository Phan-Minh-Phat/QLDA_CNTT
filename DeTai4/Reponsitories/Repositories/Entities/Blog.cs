using System;
using System.Collections.Generic;

namespace DeTai4.Reponsitories.Repositories.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? Author { get; set; }
    }
}
