using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class User
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("email_address")]
        public string EmailAddress { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("middle_name")]
        public string MiddleName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        //public int PubId { get; set; }
        //[ForeignKey("PublisherId")]
        //public Publisher Publisher { get; set; }

        [Column("hire_date")]
        public DateTime HireDate { get; set; }


    }
}
