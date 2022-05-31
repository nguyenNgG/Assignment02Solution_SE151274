using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Publisher
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
            Users = new HashSet<User>();
        }

        [Key]
        [Column("publisher_id")]
        public int PublisherId { get; set; }

        [Column("publisher_name", TypeName = "nvarchar(200)")]
        public string PublisherName { get; set; }

        [Column("city", TypeName = "nvarchar(100)")]
        public string City { get; set; }

        [Column("state", TypeName = "nvarchar(100)")]
        public string State { get; set; }

        [Column("country", TypeName = "nvarchar(100)")]
        public string Country { get; set; }

        public ICollection<Book> Books { get; set; }

        public ICollection<User> Users { get; set; }

    }
}
