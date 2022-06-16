using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject
{
    public class Publisher
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
            Users = new HashSet<User>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("publisher_id")]
        public int PublisherId { get; set; }

        [Column("publisher_name", TypeName = "nvarchar(200)")]
        [Required]
        [StringLength(190, ErrorMessage = "{0} must have between {2}-{1} characters.", MinimumLength = 1)]
        public string PublisherName { get; set; }

        [Column("city", TypeName = "nvarchar(100)")]
        [Required]
        [StringLength(90, ErrorMessage = "{0} must have between {2}-{1} characters.", MinimumLength = 1)]
        public string City { get; set; }

        [Column("state", TypeName = "nvarchar(100)")]
        [Required]
        [StringLength(90, ErrorMessage = "{0} must have between {2}-{1} characters.", MinimumLength = 1)]
        public string State { get; set; }

        [Column("country", TypeName = "nvarchar(100)")]
        [Required]
        [StringLength(90, ErrorMessage = "{0} must have between {2}-{1} characters.", MinimumLength = 1)]
        public string Country { get; set; }
        public ICollection<Book> Books { get; set; }
        public ICollection<User> Users { get; set; }

    }
}
