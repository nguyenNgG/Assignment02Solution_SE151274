using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("email_address", TypeName = "nvarchar(200)")]
        public string EmailAddress { get; set; }

        [Column("password", TypeName = "nvarchar(200)")]
        public string Password { get; set; }

        [Column("source", TypeName = "nvarchar(200)")]
        public string Source { get; set; }

        [Column("first_name", TypeName = "nvarchar(200)")]
        public string FirstName { get; set; }

        [Column("middle_name", TypeName = "nvarchar(200)")]
        public string MiddleName { get; set; }

        [Column("last_name", TypeName = "nvarchar(200)")]
        public string LastName { get; set; }

        [Column("role_id")]
        public int? RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role? Role { get; set; }

        [Column("publisher_id")]
        public int? PublisherId { get; set; }

        [ForeignKey("PublisherId")]
        public Publisher? Publisher { get; set; }

        [Column("hire_date", TypeName = "datetime2(7)")]
        public DateTime HireDate { get; set; }
    }
}
