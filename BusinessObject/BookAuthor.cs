using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject
{
    public class BookAuthor
    {
        [Column("author_id")]
        [Required]
        public int? AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public Author? Author { get; set; }

        [Column("book_id")]
        public int? BookId { get; set; }

        [ForeignKey("BookId")]
        public Book? Book { get; set; }

        [Column("author_order")]
        public int AuthorOrder { get; set; }

        [Column("royalty_percentage", TypeName = "decimal(5,2)")]
        public decimal RoyaltyPercentage { get; set; }

    }
}
