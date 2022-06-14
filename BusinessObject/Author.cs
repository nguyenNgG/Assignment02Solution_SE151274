using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        public string LastName { get; set; }

        [Column("first_name", TypeName = "nvarchar(200)")]
        public string FirstName { get; set; }

        [Column("phone", TypeName = "varchar(20)")]
        public string Phone { get; set; }

        [Column("address", TypeName = "nvarchar(200)")]
        public string Address { get; set; }

        [Column("city", TypeName = "nvarchar(100)")]
        public string City { get; set; }

        [Column("state", TypeName = "nvarchar(100)")]
        public string State { get; set; }

        [Column("zip", TypeName = "varchar(100)")]
        public string Zip { get; set; }

        [Column("email_address", TypeName = "varchar(200)")]
        public string EmailAddress { get; set; }

        [JsonIgnore]
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
