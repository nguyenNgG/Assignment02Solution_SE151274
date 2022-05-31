using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        [Column("role_id")]
        public int RoleId { get; set; }

        [Column("role_desc", TypeName = "nvarchar(200)")]
        public string RoleDesc { get; set; }

        public ICollection<User> Users { get; set; }

    }
}
