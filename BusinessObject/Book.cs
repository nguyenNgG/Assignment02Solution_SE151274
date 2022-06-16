using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject
{
    public class Book
    {
        public Book()
        {
            BookAuthors = new HashSet<BookAuthor>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("book_id")]
        public int BookId { get; set; }

        [Column("title", TypeName = "nvarchar(200)")]
        [Required]
        [StringLength(190, ErrorMessage = "{0} must have between {2}-{1} characters.", MinimumLength = 1)]
        public string Title { get; set; }

        [Column("type", TypeName = "nvarchar(100)")]
        [Required]
        [StringLength(90, ErrorMessage = "{0} must have between {2}-{1} characters.", MinimumLength = 1)]
        public string Type { get; set; }

        [Column("publisher_id")]
        public int? PublisherId { get; set; }

        [ForeignKey("PublisherId")]
        public Publisher? Publisher { get; set; }

        [Column("price", TypeName = "money")]
        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "{0} can't be lower than 0.")]
        public decimal Price { get; set; }

        [Column("advance", TypeName = "money")]
        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "{0} can't be lower than 0.")]
        public decimal Advance { get; set; }

        [Column("royalty", TypeName = "money")]
        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "{0} can't be lower than 0.")]
        public decimal Royalty { get; set; }

        [Column("ytd_sales", TypeName = "money")]
        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "{0} can't be lower than 0.")]
        public decimal YearToDateSales { get; set; }

        [Column("notes", TypeName = "nvarchar(200)")]
        [StringLength(190, ErrorMessage = "{0} must have between {2}-{1} characters.", MinimumLength = 0)]
        public string Notes { get; set; }

        [Column("published_date", TypeName = "datetime2(7)")]
        [Required]
        public DateTime PublishedDate { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
