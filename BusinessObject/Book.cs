using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public int PublisherId { get; set; }
        public decimal Price { get; set; }
        public decimal Advance { get; set; }
        public decimal Royalty { get; set; }
        public decimal YearToDateSales { get; set; }
        public string Notes { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
