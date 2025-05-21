using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMgt.Model.Entities
{
    [NotMapped]
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // Consider hashing the password
        public string FullName { get; set; }
        public bool IsActive { get; set; } // Indicates if the user is active or inactive
        public string Role { get; set; } // e.g., "Admin", "User", etc.
    }
}
