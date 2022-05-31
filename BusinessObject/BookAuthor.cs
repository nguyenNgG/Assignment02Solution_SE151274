using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class BookAuthor
    {
        [Column("author_id")]
        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public Author Author { get; set; }

        [Column("book_id")]
        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }

        [Column("author_order")]
        public int AuthorOrder { get; set; }

        [Column("royalty_percentage", TypeName = "decimal(5,2)")]
        public decimal RoyaltyPercentage { get; set; }

    }
}
