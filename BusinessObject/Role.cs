using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject
{
    public class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("role_id")]
        public int RoleId { get; set; }

        [Column("role_desc", TypeName = "nvarchar(200)")]
        public string RoleDesc { get; set; }
        public ICollection<User> Users { get; set; }

    }
}
