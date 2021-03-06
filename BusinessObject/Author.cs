using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject
{
    public class Author
    {
        public Author()
        {
            BookAuthors = new HashSet<BookAuthor>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("author_id")]
        public int AuthorId { get; set; }

        [Column("last_name", TypeName = "nvarchar(200)")]
        [Required]
        [StringLength(190, ErrorMessage = "{0} must have between {2}-{1} characters.", MinimumLength = 1)]
        public string LastName { get; set; }

        [Column("first_name", TypeName = "nvarchar(200)")]
        [Required]
        [StringLength(190, ErrorMessage = "{0} must have between {2}-{1} characters.", MinimumLength = 1)]
        public string FirstName { get; set; }

        [Column("phone", TypeName = "varchar(20)")]
        [Required]
        [StringLength(20, ErrorMessage = "{0} must have between {2}-{1} characters.", MinimumLength = 1)]
        public string Phone { get; set; }

        [Column("address", TypeName = "nvarchar(200)")]
        [Required]
        [StringLength(190, ErrorMessage = "{0} must have between {2}-{1} characters.", MinimumLength = 1)]
        public string Address { get; set; }

        [Column("city", TypeName = "nvarchar(100)")]
        [Required]
        [StringLength(90, ErrorMessage = "{0} must have between {2}-{1} characters.", MinimumLength = 1)]
        public string City { get; set; }

        [Column("state", TypeName = "nvarchar(100)")]
        [Required]
        [StringLength(90, ErrorMessage = "{0} must have between {2}-{1} characters.", MinimumLength = 1)]
        public string State { get; set; }

        [Column("zip", TypeName = "varchar(100)")]
        [Required]
        [StringLength(90, ErrorMessage = "{0} must have between {2}-{1} characters.", MinimumLength = 1)]
        public string Zip { get; set; }

        [Column("email_address", TypeName = "varchar(200)")]
        [Required]
        [StringLength(190, ErrorMessage = "{0} must have between {2}-{1} characters.", MinimumLength = 1)]
        public string EmailAddress { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
